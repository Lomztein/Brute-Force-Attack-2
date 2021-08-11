using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public abstract class TargetEvaluator
    {
        public bool Invert;

        public float Evaluate (GameObject source, Collider2D target)
        {
            float value = DoEvaluate(source, target);
            if (Invert)
            {
                value *= -1;
            }
            return value;
        }

        protected abstract float DoEvaluate(GameObject source, Collider2D target);
    }
}
