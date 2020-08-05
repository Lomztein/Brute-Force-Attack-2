using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public class GradientSerializer : EngineObjectSerializerBase<Gradient>
    {
        public override Gradient DeserializeValue(JToken value)
        {
            ColorSerializer serializer = new ColorSerializer();
            Gradient gradient = new Gradient();

            gradient.mode = value["Mode"].ToObject<GradientMode>();
            gradient.colorKeys = value["ColorKeys"].Select(x => new GradientColorKey((Color)serializer.Deserialize(x["Color"]), x["Time"].ToObject<float>())).ToArray();
            gradient.alphaKeys = value["AlphaKeys"].Select(x => new GradientAlphaKey(x["Alpha"].ToObject<float>(), x["Time"].ToObject<float>())).ToArray();

            return gradient;
        }

        public override JToken Serialize(Gradient value)
        {
            ColorSerializer serializer = new ColorSerializer();

            return new JObject()
            {
                { "ColorKeys", new JArray(value.colorKeys.Select(x => new JObject ()
                {
                    { "Time", new JValue (x.time) },
                    { "Color", serializer.Serialize(x.color) }
                }
                ))},
                { "AlphaKeys", new JArray(value.alphaKeys.Select(x => new JObject () {
                    { "Time", new JValue (x.time) },
                    { "Alpha", new JValue(x.alpha) }
                }
                ))},
                { "Mode", new JValue((int)value.mode) }
            };
        }
    }
}
