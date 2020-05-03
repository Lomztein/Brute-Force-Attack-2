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
    public class EnemyTargetEvaluator : ITargetEvaluator<Collider2D>
    {
        private Color _targetColor;
        private ITargetEvaluator<Enemy> _internalEvaluator;

        public EnemyTargetEvaluator(Color targetColor, ITargetEvaluator<Enemy> internalEvaluator)
        {
            _targetColor = targetColor;
            _internalEvaluator = internalEvaluator;
        }

        public float Evaluate(Collider2D target)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            float baseValue = _internalEvaluator.Evaluate(enemy);
            return baseValue + (enemy.Color.Get() == _targetColor ? 100000f : 0f);
        }
    }
}
