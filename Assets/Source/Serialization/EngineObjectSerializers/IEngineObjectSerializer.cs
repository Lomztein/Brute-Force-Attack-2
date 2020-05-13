using System;
using Newtonsoft.Json.Linq;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public interface IEngineObjectSerializer
    {
        bool CanConvert(Type objectType);
        object Deserialize(JToken value);
        JToken Serialize(object value);
    }
}