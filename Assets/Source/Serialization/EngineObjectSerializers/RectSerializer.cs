using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public class RectSerializer : EngineObjectSerializerBase<Rect>
    {
        public override Rect DeserializeValue(JToken value)
        {
            return new Rect(
                value["X"].ToObject<float>(),
                value["Y"].ToObject<float>(),
                value["Width"].ToObject<float>(),
                value["Height"].ToObject<float>()
                );
        }

        public override JToken Serialize(Rect value)
        {
            return new JObject()
            {
                { "X", value.x },
                { "Y", value.y },
                { "Width", value.width },
                { "Height", value.height }
            };
        }
    }
}
