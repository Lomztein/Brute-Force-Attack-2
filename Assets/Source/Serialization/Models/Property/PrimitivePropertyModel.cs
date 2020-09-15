using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Models.Property
{
    public class PrimitivePropertyModel : IPropertyModel
    {
        public PrimitivePropertyModel () { }

        public PrimitivePropertyModel (JToken value)
        {
            Value = value;
        }

        public PrimitivePropertyModel (object value)
        {
            Value = JToken.FromObject(value);
        }

        public JToken Value { get; private set; } // Consider replacing with considered DataStruct common data library interface.

        public object ToObject(Type type) => Value.ToObject(type);

        public T ToObject<T>() => Value.ToObject<T>();
    }
}
