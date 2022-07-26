using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets
{
    [CreateAssetMenu(fileName = "New Structure Category", menuName = "BFA2/Structure Category")]
    public class StructureCategory : ScriptableObject
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;

        public StructureCategory(string name, string desc)
        {
            Name = name;
            Description = desc;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

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
