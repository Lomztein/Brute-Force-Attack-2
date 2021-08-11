using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Targeting
{
    public class ColoredTargetEvaluator : TargetEvaluator
    {
        private const float _colorWeight = 100000f;
        private ColorTargetEvaluator _colorEvaluator = new ColorTargetEvaluator();

        [SerializeReference, SR, ModelProperty]
        public TargetEvaluator Evaluator;

        public void SetColor(BFA2.Colorization.Color color) => _colorEvaluator.TargetColor = color;

        protected override float DoEvaluate(GameObject source, Collider2D target)
            => Evaluator.Evaluate(source, target) + _colorEvaluator.Evaluate(source, target) * _colorWeight;
    }
}