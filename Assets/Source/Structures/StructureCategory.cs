using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets
{
    public static class StructureCategories
    {
        public static StructureCategory TargetFinder => new StructureCategory("Target Finder", "Common root component. Finds and provides targets to other components.");
        public static StructureCategory Targeter => new StructureCategory("Targeter", "Targets the target provided by an underlying target finder.");
        public static StructureCategory Weapon => new StructureCategory("Weapon", "Fires towards targets with the intend to destroy.");
        public static StructureCategory Connector => new StructureCategory("Connector", "Connects components to a target component so that they may support it.");
        public static StructureCategory Structural => new StructureCategory("Structural", "Does nothing except provide space for other components.");
        public static StructureCategory Utility => new StructureCategory("Utility", "Provides utilities to other components.");
        public static StructureCategory Misc => new StructureCategory("Miscellaneous", "Miscellaneous components with unique functionality.");
    }

    public class StructureCategory
    {
        public string Name;
        public string Description;

        public StructureCategory(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public override bool Equals(object obj)
        {
            if (obj is StructureCategory category)
            {
                return Name.Equals(category.Name);
            }
            return false;
        }

        public override int GetHashCode() // uh okay
        {
            var hashCode = -2030050305;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}
