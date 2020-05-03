using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class CompositeTargetEvaluator<T> : ITargetEvaluator<T>
    {
        private ITargetEvaluator<T>[] _evaluators;
        
        public CompositeTargetEvaluator (params ITargetEvaluator<T>[] evaluators)
        {
            _evaluators = evaluators;
        }

        public float Evaluate(T target)
        {
            return _evaluators.Sum(x => x.Evaluate (target));
        }
    }
}