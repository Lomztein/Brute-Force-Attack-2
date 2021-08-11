using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class RandomTargetEvaluator : TargetEvaluator
    {
        protected override float DoEvaluate(GameObject source, Collider2D target)
            => UnityEngine.Random.Range(-1000f, 1000f);
    }
}
