using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Targeting;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.ContextMenu.SubMenus;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets
{
    public class TargetEvaluatorChanger : MonoBehaviour, IContextMenuOptionProvider
    {
        public TargetEvaluatorOption[] EvaluatorOptions;
        public ContentSpriteReference Sprite;
        public GameObject ChangerSubmenu;

        [SerializeField]
        private List<int> _evaluatorIndex;
        public TargetEvaluatorOption[] CurrentEvaluator => _evaluatorIndex.Select(x => EvaluatorOptions[x]).ToArray();

        private void Awake()
        {
            for (int i = 0; i < EvaluatorOptions.Length; i++)
            {
                EvaluatorOptions[i] = Instantiate(EvaluatorOptions[i]); // Clone each to avoid weird shit.
            }
            GetComponent<Structure>().HierarchyChanged += TargetEvaluatorChanger_Changed;
            _evaluatorIndex = GetDefaultOptionIndexes();
        }

        private void TargetEvaluatorChanger_Changed(Structure structure, GameObject obj, object source)
        {
            StartCoroutine(DelayedUpdateEvaluator(false));
        }

        private void Start()
        {
            StartCoroutine(DelayedUpdateEvaluator(false));
        }

        private IEnumerator DelayedUpdateEvaluator (bool overrideCurrent)
        {
            yield return new WaitForEndOfFrame();
            UpdateEvaluator(overrideCurrent);
        }

        private void UpdateEvaluator(bool overrideCurrent)
        {
            foreach (TurretBase b in GetComponentsInChildren<TurretBase>())
            {
                if (b.GetEvaluator() == null || !b.GetEvaluator().Any() || overrideCurrent)
                {
                    b.SetEvaluators(CurrentEvaluator.Select(x => x.Evaluator));
                }
            }
        }

        private GameObject InstantiateChangerSubmenu ()
        {
            GameObject changer = Instantiate(ChangerSubmenu);
            changer.GetComponent<TargetPrioritySubMenu>().Assign(this);
            return changer;
        }

        private List<int> GetDefaultOptionIndexes ()
        {
            int colorOptionIndex = GetColorOptionIndex(GetPriorityColor());
            if (colorOptionIndex == -1)
            {
                return new List<int> { 0 };
            }
            else
            {
                return new List<int>() {
                    GetColorOptionIndex(GetPriorityColor()), 
                    0
                };
            }
        }

        private int GetColorOptionIndex (Colorization.Color color)
        {
            for (int i = 0; i < EvaluatorOptions.Length; i++)
            {
                if (EvaluatorOptions[i].Evaluator is ColorTargetEvaluator colorEvaluator && colorEvaluator.TargetColor == color)
                {
                    return i;
                }
            }
            return -1;
        }

        private Colorization.Color GetPriorityColor ()
        {
            var groups = GetComponentsInChildren<TurretWeapon>().GroupBy(x => x.GetColor());
            
            int most = 0;
            Colorization.Color color = Colorization.Color.Blue;

            foreach (var group in groups)
            {
                int count = group.Count();
                if (count > most)
                {
                    most = count;
                    color = group.Key;
                }
            }

            return color;
        }

        public void ChangeEvaluator(int slot, int change)
        {
            _evaluatorIndex[slot] += change;
            _evaluatorIndex[slot] %= EvaluatorOptions.Length;
            if (_evaluatorIndex[slot] < 0) _evaluatorIndex[slot] = EvaluatorOptions.Length - 1;
            UpdateEvaluator(true);
            TooltipController.ForceResetToolTip();
        }

        public void SetEvaluator(int slot, int value)
        {
            _evaluatorIndex[slot] = value;
            UpdateEvaluator(true);
            TooltipController.ForceResetToolTip();
        }

        public int AddSlot ()
        {
            _evaluatorIndex.Add(0);
            return _evaluatorIndex.Count - 1;
        }

        public void RemoveSlot(int index)
        {
            _evaluatorIndex.RemoveAt(index);
        }

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption(Sprite.Get, () => UI.ContextMenu.ContextMenu.Side.Left)
                .WithSubMenu(InstantiateChangerSubmenu)
                .WithToolTip(() => SimpleToolTip.InstantiateToolTip("Target prioritization")),
            };
        }
    }
}
