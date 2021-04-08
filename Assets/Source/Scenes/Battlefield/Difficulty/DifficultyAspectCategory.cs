using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public class DifficultyAspectCategory
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
    
        public DifficultyAspectCategory (string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public static DifficultyAspectCategory Enemies = new DifficultyAspectCategory("Enemies", "Settings relating to enemies.");
        public static DifficultyAspectCategory Resources = new DifficultyAspectCategory("Resources", "Settings relating to resources.");

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
