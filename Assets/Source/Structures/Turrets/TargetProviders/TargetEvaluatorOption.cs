using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Structures.Turrets.TargetProviders
{
    [CreateAssetMenu(fileName = "NewTargetEvaluatorOption", menuName = "BFA2/Turrets/Target Evaluator Option")]
    public class TargetEvaluatorOption : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;

        [SerializeReference, SR, ModelProperty]
        public TargetEvaluator Evaluator;
        [ModelProperty]
        public ContentSpriteReference Sprite;
    }
}
