using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models
{
    public abstract class ValueModel
    {
        public Type ValueType { get; set; }

        /// <summary>
        /// A property type is considered implicit if the type can be fetched from somewhere else, such as a field or array during assembly.
        /// If it cannot be fetched from somewhere else, for instance if it is a subtype, then it must be explicit.
        /// </summary>
        public bool IsTypeImplicit => ValueType == null;

        public ValueModel MakeImplicit()
        {
            ValueType = null;
            return this;
        }

        public ValueModel MakeExplicit(Type type)
        {
            ValueType = type;
            return this;
        }

        public override string ToString()
        {
            return IsTypeImplicit ? "" : ValueType.Name;
        }
    }
}
