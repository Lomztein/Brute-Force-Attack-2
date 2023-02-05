using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Targeting
{
    public class ColorTargetEvaluator : TargetEvaluator
    {
        public Color TargetColor;

        protected override float DoEvaluate(GameObject source, Collider2D target)
        {
            Enemy enemy = target.GetComponentInParent<Enemy>();
            return enemy.Color == TargetColor ? 1f : -1f;
        }
    }
}
