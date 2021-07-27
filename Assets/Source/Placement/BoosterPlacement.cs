using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class BoosterPlacement : ISimplePlacement
    {
        public event Action<GameObject> OnPlaced;
        public event Action OnFinished;

        private GameObject _obj;
        private BoosterModBroadcaster _booster;

        private GameObject _target;
        private LayerMask _layerMask = LayerMask.GetMask("Structure");

        private Func<bool>[] _placeRequirements;

        public BoosterPlacement(params Func<bool>[] placeRequirements)
        {
            _placeRequirements = placeRequirements;
        }

        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _booster = obj.GetComponent<BoosterModBroadcaster>();
            return _booster != null;
        }

        public bool Place()
        {
            if (_target != null)
            {
                if (_placeRequirements.All(x => x() == true))
                {
                    GameObject cardGO = UnityEngine.Object.Instantiate(_obj, _target.transform.root);
                    BoosterModBroadcaster card = cardGO.GetComponent<BoosterModBroadcaster>();
                    OnPlaced?.Invoke(cardGO);
                    PlacementController.Instance.CancelAll();
                    return true;
                }
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            var options = Physics2D.OverlapPointAll(position, _layerMask);

            if (options.Length == 1)
            {
                _target = options.First().transform.root.gameObject;
            }
            else
            {
                _target = null;
            }
            return true;
        }

        public override string ToString()
        {
            if (_target != null)
            {
                return $"Insert {_booster.Mod.Name} into {_target.GetComponent<Structure>().Name}?";
            }
            else
            {
                return _booster.Mod.Name;
            }
        }

        public bool Finish()
        {
            UnityEngine.Object.Destroy(_obj);
            OnFinished?.Invoke();
            return true;
        }

        public void Init()
        {
        }
    }
}
