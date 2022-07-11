using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Scenes.Battlefield.Difficulty.Aspects;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.UI.Menus.PropertyMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Scenes.Battlefield.Difficulty
{
    [CreateAssetMenu(fileName = "NewDifficulty", menuName = "BFA2/Difficulty/Difficulty")]
    public class Difficulty : ScriptableObject, IHasProperties
    {
        [ModelProperty]
        public string Name;
        [ModelProperty, TextArea]
        public string Description;
        [ModelAssetReference]
        public DifficultyGrade Grade;
        [ModelProperty]
        public string Identifier;
        public bool IsModified;

        [ModelProperty, SerializeReference, SR]
        private IDifficultyAspect[] Aspects = new IDifficultyAspect[]
        {
            new ResourcesDifficultyAspect()
            {
                Amount = new ResourcesDifficultyAspect.ResourceAmount[]
                {
                    new ResourcesDifficultyAspect.ResourceAmount()
                    {
                        ResourceIdentifier = "Core.Credits",
                        Value = 750
                    }
                }
            }
        };

        public IDifficultyAspect[] GetAspects() => Aspects.ToArray();
        public void SetAspects(IEnumerable<IDifficultyAspect> aspects) => Aspects = aspects.ToArray();

        public void Apply ()
        {
            foreach (var aspect in Aspects)
            {
                aspect.Apply();
            }
        }

        public static Difficulty Combine (IEnumerable<Difficulty> difficulties)
        {
            List<IDifficultyAspect> list = new List<IDifficultyAspect>();

            foreach (Difficulty difficulty in difficulties)
            {
                 list.AddRange(difficulty.GetAspects());
            }
            Difficulty first = Instantiate(difficulties.First());
            first.SetAspects(list);
            return first;
        }

        public void AddPropertiesTo(PropertyMenu menu)
        {
            foreach (IDifficultyAspect aspect in Aspects)
            {
                if (aspect is IHasProperties hasProperties)
                {
                    hasProperties.AddPropertiesTo(menu);
                }
            }
        }
    }

    public class DifficultyComparer : IComparer<Difficulty>
    {
        public int Compare(Difficulty x, Difficulty y)
        {
            int xGrade = x.Grade.DifficultyIndex;
            int yGrade = y.Grade.DifficultyIndex;
            return xGrade - yGrade;
        }
    }
}
