using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects
{
    [CreateAssetMenu(fileName = "NewDifficultyAspectCategory", menuName = "BFA2/Difficulty/Aspect Category")]
    public class DifficultyAspectCategory : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Identifier;

        private const string CATEGORY_PATH = "*/Difficulty/AspectCategories";
        private static ContentArrayReference<DifficultyAspectCategory> _dífficulties = new ContentArrayReference<DifficultyAspectCategory>(CATEGORY_PATH);

        public static DifficultyAspectCategory GetDifficulty(string identifier) => _dífficulties.FirstOrDefault(x => x.Identifier == identifier);

        public override bool Equals(object obj)
        {
            return obj is DifficultyAspectCategory category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
