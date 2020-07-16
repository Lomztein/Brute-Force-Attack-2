using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public class ColorSerializer : EngineObjectSerializerBase<Color>
    {
        public override Color DeserializeValue(JToken value)
        {
            return new Color(value["Red"].ToObject<float>(), value["Green"].ToObject<float>(), value["Blue"].ToObject<float>(), value["Alpha"].ToObject<float>());
        }

        public override JToken Serialize(Color value)
        {
            return new JObject()
            {
                {"Red", value.r },
                {"Green", value.g },
                {"Blue", value.b },
                {"Alpha", value.a }
            };
        }
    }
}
