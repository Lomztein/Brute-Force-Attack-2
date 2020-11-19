using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.Models
{
    public class PrimitiveModel : ValueModel
    {
        public JToken Value { get; private set; } // Consider replacing with considered DataStruct common data library interface.
        public PrimitiveModel () { }

        public PrimitiveModel (object value)
        {
            if (value == null)
            {
                Value = JValue.CreateNull();
            }
            else
            {
                Value = JToken.FromObject(value);
            }
        }

        public static PrimitiveModel FromToken (JToken token)
        {
            return new PrimitiveModel()
            {
                Value = token
            };
        }

        public object ToObject(Type type) => Value.ToObject(type);

        public T ToObject<T>() => Value.ToObject<T>();

        public override string ToString()
        {
            return base.ToString() + " " + Value.ToString();
        }
    }
}
