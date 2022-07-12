using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Targeting;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
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

        private int _evaluatorIndex = 0;
        private TargetEvaluatorOption CurrentEvaluator => EvaluatorOptions[_evaluatorIndex];

        private void Awake()
        {
            for (int i = 0; i < EvaluatorOptions.Length; i++)
            {
                EvaluatorOptions[i] = Instantiate(EvaluatorOptions[i]); // Clone each to avoid weird shit.
            }
            GetComponent<Structure>().HierarchyChanged += TargetEvaluatorChanger_Changed;
        }

        private void TargetEvaluatorChanger_Changed(Structure structure, GameObject obj, object source)
        {
            StartCoroutine(DelayedUpdateEvaluator());
        }

        private void Start()
        {
            StartCoroutine(DelayedUpdateEvaluator());
        }

        private IEnumerator DelayedUpdateEvaluator ()
        {
            yield return new WaitForEndOfFrame();
            UpdateEvaluator();
        }

        private void UpdateEvaluator()
        {
            if (CurrentEvaluator.Evaluator is ColoredTargetEvaluator evaluator)
            {
                evaluator.SetColor(GetPriorityColor());
            }

            foreach (TurretBase b in GetComponentsInChildren<TurretBase>())
            {
                b.SetEvaluator(CurrentEvaluator.Evaluator);
            }
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

        private bool NextEvaluator()
        {
            _evaluatorIndex++;
            _evaluatorIndex %= EvaluatorOptions.Length;
            UpdateEvaluator();
            return false;
        }

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption(CurrentEvaluator.Sprite.Get, () => null, () => UI.ContextMenu.ContextMenu.Side.Left, NextEvaluator, () => true, () => SimpleToolTip.InstantiateToolTip($"Targeting: {CurrentEvaluator.Name}", CurrentEvaluator.Description))
            };
        }
    }
}
