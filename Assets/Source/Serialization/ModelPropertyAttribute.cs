using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ModelPropertyAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool HasCustomName => !string.IsNullOrEmpty(Name);

        public ModelPropertyAttribute () { }

        public ModelPropertyAttribute (string name)
        {
            Name = name;
        }
    }
}
