using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public abstract class EngineObjectSerializerBase<T> : IEngineObjectSerializer
    {
        public bool CanConvert(Type objectType) => objectType == typeof(T);
        public object Deserialize(JToken value)
        {
            return DeserializeValue(value);
        }

        public abstract T DeserializeValue(JToken value);

        public JToken Serialize(object value)
        {
            return Serialize((T)value);
        }

        public abstract JToken Serialize(T value);
    }
}
