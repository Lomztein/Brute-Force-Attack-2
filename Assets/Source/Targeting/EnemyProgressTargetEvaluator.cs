using Lomztein.BFA2.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class EnemyProgressTargetEvaluator : TargetEvaluator
    {
        protected override float DoEvaluate(GameObject source, Collider2D target)
            => target.GetComponentInParent<Enemy>().PathIndex;
    }
}
