using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class DistanceTargetEvaluator : TargetEvaluator
    {
        protected override float DoEvaluate(GameObject source, Collider2D target)
            => Vector3.SqrMagnitude(target.transform.position - source.transform.position);
    }
}
