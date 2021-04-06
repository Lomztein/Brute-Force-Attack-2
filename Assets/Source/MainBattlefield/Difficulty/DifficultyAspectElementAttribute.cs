using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.MainBattlefield.Difficulty
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DifficultyAspectElementAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public DifficultyAspectElementAttribute(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}
