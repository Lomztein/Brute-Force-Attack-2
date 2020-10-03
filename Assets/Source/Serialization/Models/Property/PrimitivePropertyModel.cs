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
        public JToken Value { get; private set; } // Consider replacing with considered DataStruct common data library interface.
        public Type Type { get; private set; }

        public PrimitivePropertyModel () { }

        public PrimitivePropertyModel (object value)
        {
            if (value == null)
            {
                Value = JValue.CreateUndefined();
            }
            else
            {
                Value = JToken.FromObject(value);
            }
            Type = Value?.GetType();
        }

        public object ToObject(Type type) => Value.ToObject(type);

        public T ToObject<T>() => Value.ToObject<T>();
    }
}
