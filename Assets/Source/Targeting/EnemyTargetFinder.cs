using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public class EnemyTargetFinder : TargetFinder
    {
        private void Awake()
        {
            SetEvaluator(new EnemyTargetEvaluator(GetComponentInChildren<IColorProvider>().GetColor(), new TargetEvaluator<Enemy>(x => -Vector3.SqrMagnitude(x.transform.position - transform.position))));
        }
    }
}
