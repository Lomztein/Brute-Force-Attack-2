using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class ModulePlacement : ISimplePlacement
    {
        public event Action<GameObject> OnPlaced;
        public event Action OnFinished;

        private GameObject _obj;
        private TurretAssemblyModule _module;
        private SpriteRenderer _spriteRenderer;

        private GameObject _target;
        private LayerMask _layerMask = LayerMask.GetMask("Structure");

        private Func<bool>[] _placeRequirements;

        public ModulePlacement(params Func<bool>[] placeRequirements)
        {
            _placeRequirements = placeRequirements;
        }

        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _module = obj.GetComponent<TurretAssemblyModule>();
            _obj.SetActive(false);

            GameObject renderer = new GameObject("ModuleSprite");
            _spriteRenderer = renderer.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _module.Item.Sprite.Get();
            _spriteRenderer.color = _module.Item.SpriteTint;

            return _module != null;
        }

        public bool Place()
        {
            if (_target != null)
            {
                if (_placeRequirements.All(x => x() == true))
                {
                    GameObject cardGO = UnityEngine.Object.Instantiate(_obj, _target.transform.root);
                    RootModBroadcaster card = cardGO.GetComponent<RootModBroadcaster>();
                    card.gameObject.SetActive(true);
                    TurretAssemblyModule module = cardGO.GetComponent<TurretAssemblyModule>();
                    _target.GetComponent<TurretAssembly>().AddModule(module);

                    OnPlaced?.Invoke(cardGO);
                    PlacementController.Instance.CancelAll();
                    return true;
                }
            }
            return false;
        }

        private bool CanModAnyChild(Mod mod, GameObject root)
        {
            return root.GetComponentsInChildren<IModdable>().Any(x => mod.CanMod(x));
        }

        public bool ToPosition(Vector2 position)
        {
            var options = Physics2D.OverlapPointAll(position, _layerMask)
                .Select(x => x.transform.root).Where(x => x.TryGetComponent(out TurretAssembly assembly)); // Limit modules to only turret assemblies for the time being. 
                
            _spriteRenderer.transform.position = (Vector3)position + Vector3.back * 9f;

            if (options.Count() == 1)
            {
                var option = options.FirstOrDefault().GetComponent<TurretAssembly>();
                if (CanModAnyChild(_module.Item.Mod, option.transform.root.gameObject)) {
                    
                    if (option.HasRoomFor(_module.Item.ModuleSlots))
                    {
                        _target = option.transform.root.gameObject;
                        _spriteRenderer.transform.position = (Vector3)(Vector2)_target.transform.position + Vector3.back * 9f;
                        ForcedTooltipUpdater.SetTooltip(() => SimpleToolTip.InstantiateToolTip(_module.Item.Name + " -> " + _target.GetComponent<TurretAssembly>().Name), "OptionWithRoom");
                    }
                    else
                    {
                        ForcedTooltipUpdater.SetTooltip(() => SimpleToolTip.InstantiateToolTip("Cannot insert into " + option.Name, $"Not enough module slots. {_module.Item.ModuleSlots} needed, {option.FreeModuleSlots()} available."), "OptionsWithoutRoom");
                    }
                }
                else
                {
                    ForcedTooltipUpdater.SetTooltip(() => SimpleToolTip.InstantiateToolTip("Cannot insert into " + option.Name, $"Module is incompatable."), "OptionIncompatable");
                }
            }
            else
            {
                _target = null;
                ForcedTooltipUpdater.ResetTooltip();
            }
            return true;
        }

        public override string ToString()
        {
            if (_target != null)
            {
                return $"Insert {_module.Item.Name} into {_target.GetComponent<Structure>().Name}?";
            }
            else
            {
                return _module.Item.Name;
            }
        }

        public bool Finish()
        {
            UnityEngine.Object.Destroy(_obj);
            UnityEngine.Object.Destroy(_spriteRenderer.gameObject);
            ForcedTooltipUpdater.ResetTooltip();

            OnFinished?.Invoke();
            return true;
        }

        public void Init()
        {
        }

        public bool ToRotation(Quaternion rotation)
        {
            return true;
        }

        public bool Flip()
        {
            return true;
        }
    }
}
