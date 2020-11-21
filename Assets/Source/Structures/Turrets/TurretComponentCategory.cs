using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets
{
    public static class TurretComponentCategories
    {
        public static TurretComponentCategory TargetFinder => new TurretComponentCategory("Target Finder", "Common root component. Finds and provides targets to other components.");
        public static TurretComponentCategory Targeter => new TurretComponentCategory("Targeter", "Targets the target provided by an underlying target finder.");
        public static TurretComponentCategory Weapon => new TurretComponentCategory("Weapon", "Fires towards targets with the intend to destroy.");
        public static TurretComponentCategory Connector => new TurretComponentCategory("Connector", "Connects components to a target component so that they may support it.");
        public static TurretComponentCategory Structural => new TurretComponentCategory("Structural", "Does nothing except provide space for other components.");
        public static TurretComponentCategory Utility => new TurretComponentCategory("Utility", "Provides utilities to other components.");
        public static TurretComponentCategory Misc => new TurretComponentCategory("Miscellaneous", "Miscellaneous components with unique functionality.");
    }

    public class TurretComponentCategory
    {
        public string Name;
        public string Description;

        public TurretComponentCategory(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public override bool Equals(object obj)
        {
            if (obj is TurretComponentCategory category)
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
