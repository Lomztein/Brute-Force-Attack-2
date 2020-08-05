using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.EngineObjectSerializers
{
    public class AnimationCurveSerializer : EngineObjectSerializerBase<AnimationCurve>
    {
        public override AnimationCurve DeserializeValue(JToken value)
        {
            JArray array = value as JArray;
            AnimationCurve curve = new AnimationCurve(
                array.Select(x => new Keyframe(
                    x["Time"].ToObject<float>(),
                    x["Value"].ToObject<float>(),
                    x["InTangent"].ToObject<float>(),
                    x["OutTangent"].ToObject<float>(),
                    x["InWeight"].ToObject<float>(),
                    x["OutWeight"].ToObject<float>()
                )).ToArray());
            return curve;
        }

        public override JToken Serialize(AnimationCurve value)
        {
            return new JArray(value.keys.Select(x => new JObject()
            { 
                { "Time", x.time },
                { "Value", x.value },
                { "InTangent", x.inTangent },
                { "OutTangent", x.outTangent },
                { "InWeight", x.inWeight },
                { "OutWeight", x.outWeight },
                { "WeightedMode", (int)x.weightedMode },
            }));
        }
    }
}
