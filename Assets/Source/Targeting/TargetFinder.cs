using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class TargetFinder : MonoBehaviour, ITargetFinder
    {
        private ITargetEvaluator<Collider2D> _evalutator;

        public void SetEvaluator (ITargetEvaluator<Collider2D> evaluator)
        {
            _evalutator = evaluator;
        }

        public Transform FindTarget(Collider2D[] options)
        {
            Collider2D best = options.FirstOrDefault();
            float bestValue = -Mathf.Infinity;

            foreach (Collider2D option in options)
            {
                float value = _evalutator.Evaluate(option);

                if (value > bestValue)
                {
                    best = option;
                    bestValue = value;
                }
            }

            return best?.transform;
        }
    }
}
