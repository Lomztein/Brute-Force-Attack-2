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
    public class Vector2Serializer : EngineObjectSerializerBase<Vector2>
    {
        public override Vector2 DeserializeValue(JToken value)
        {
            return new Vector2(value["X"].ToObject<float>(), value["Y"].ToObject<float>());
        }

        public override JToken Serialize(Vector2 value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y }
                };
    }

    public class Vector2IntSerializer : EngineObjectSerializerBase<Vector2Int>
    {
        public override Vector2Int DeserializeValue(JToken value)
        {
            return new Vector2Int(value["X"].ToObject<int>(), value["Y"].ToObject<int>());
        }

        public override JToken Serialize(Vector2Int value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y }
                };
    }

    public class Vector3Serializer : EngineObjectSerializerBase<Vector3>
    {
        public override Vector3 DeserializeValue(JToken value)
        {
            return new Vector3(
                value["X"].ToObject<float>(),
                value["Y"].ToObject<float>(),
                value["Z"].ToObject<float>());
        }

        public override JToken Serialize(Vector3 value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y },
                    { "Z", value.z }
                };
    }

    public class Vector3IntSerializer : EngineObjectSerializerBase<Vector3Int>
    {
        public override Vector3Int DeserializeValue(JToken value)
        {
            return new Vector3Int(
                value["X"].ToObject<int>(),
                value["Y"].ToObject<int>(),
                value["Z"].ToObject<int>());
        }

        public override JToken Serialize(Vector3Int value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y },
                    { "Z", value.z }
                };
    }

    public class Vector4Serializer : EngineObjectSerializerBase<Vector4>
    {
        public override Vector4 DeserializeValue(JToken value)
        {
            return new Vector4(
                value["X"].ToObject<float>(),
                value["X"].ToObject<float>(),
                value["Z"].ToObject<float>(),
                value["W"].ToObject<float>());
        }

        public override JToken Serialize(Vector4 value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y },
                    { "Z", value.z },
                    { "W", value.w }
                };
    }

    public class QuaternionSerializer : EngineObjectSerializerBase<Quaternion>
    {
        public override Quaternion DeserializeValue(JToken value)
        {
            return new Quaternion(
                value["X"].ToObject<float>(),
                value["X"].ToObject<float>(),
                value["Z"].ToObject<float>(),
                value["W"].ToObject<float>());
        }

        public override JToken Serialize(Quaternion value)
            => new JObject()
                {
                    { "X", value.x },
                    { "Y", value.y },
                    { "Z", value.z },
                    { "W", value.w }
                };
    }
}
