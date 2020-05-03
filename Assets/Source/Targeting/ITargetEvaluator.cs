using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public interface ITargetEvaluator<T>
    {
        float Evaluate(T target);
    }
}