using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public abstract class PropertyModel
    {
        public Type PropertyType { get; set; }

        /// <summary>
        /// A property type is considered implicit if the type can be fetched from somewhere else, such as a field or array during assembly.
        /// If it cannot be fetched from somewhere else, for instance if it is a subtype, then it must be explicit.
        /// </summary>
        public bool ImplicitType { get; set; } = true;

        public PropertyModel MakeImplicit()
        {
            ImplicitType = true;
            return this;
        }

        public PropertyModel MakeExplicit()
        {
            ImplicitType = false;
            return this;
        }
    }
}
