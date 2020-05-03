using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class TargetEvaluator<T> : ITargetEvaluator<T>
    {
        Func<T, float> _function;

        public TargetEvaluator (Func<T, float> function)
        {
            _function = function;
        }

        public float Evaluate(T target)
        {
            return _function(target);
        }
    }
}
