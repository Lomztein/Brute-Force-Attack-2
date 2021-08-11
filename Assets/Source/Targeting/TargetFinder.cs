using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Targeting
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeReference, SR, ModelProperty]
        private TargetEvaluator _evalutator;

        public void SetEvaluator (TargetEvaluator evaluator)
        {
            _evalutator = evaluator;
        }

        public Transform FindTarget(GameObject source, IEnumerable<Collider2D> options)
        {
            Collider2D best = options.FirstOrDefault();
            float bestValue = -Mathf.Infinity;

            foreach (Collider2D option in options)
            {
                float value = _evalutator?.Evaluate(source, option) ?? 0;

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
