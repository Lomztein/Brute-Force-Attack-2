using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class DirectionTargetEvaluator : TargetEvaluator
    {
        protected override float DoEvaluate(GameObject source, Collider2D target)
        {
            Vector3 dir = (target.transform.position - source.transform.position).normalized;
            return Vector3.Dot(source.transform.right, dir);
        }
    }
}
