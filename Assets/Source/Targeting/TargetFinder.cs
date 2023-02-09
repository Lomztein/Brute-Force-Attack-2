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
        public TargetEvaluator[] TargetEvaluators;

        public void SetEvaluator (IEnumerable<TargetEvaluator> evaluators)
        {
            TargetEvaluators = evaluators.ToArray();
        }

        public Transform FindTarget(GameObject source, IEnumerable<Collider2D> options)
        {
            var remaining = new Queue<Collider2D>(options);
            foreach (var evaluator in TargetEvaluators)
            {
                var next = new Queue<Collider2D>();
                float bestValue = -Mathf.Infinity;

                while (remaining.Count > 0)
                {
                    var option = remaining.Dequeue();
                    float value = evaluator.Evaluate(source, option);

                    if (Mathf.Abs(value - bestValue) < Mathf.Epsilon)
                    {
                        next.Enqueue(option);
                    }
                    else if (value > bestValue)
                    {
                        next.Clear();
                        next.Enqueue(option);
                        bestValue = value;
                    }
                }
                remaining = next;
            }

            if (remaining.TryDequeue(out Collider2D val))
            {
                return val.transform;
            }
            return null;
        }
    }
}
