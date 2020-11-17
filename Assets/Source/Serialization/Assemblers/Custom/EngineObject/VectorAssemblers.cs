using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class Vector2Assembler : EngineObjectAssemblerBase<Vector2>
    {
        public override Vector2 AssembleValue(ObjectModel value)
        {
            return new Vector2(value.GetValue<float>("X"), value.GetValue<float>("Y"));
        }

        public override ObjectModel DisassembleValue(Vector2 value)
            => new ObjectModel(typeof(Vector2))
            {
                { "X", value.x },
                { "Y", value.y },
            };
    }

    public class Vector2IntAssembler : EngineObjectAssemblerBase<Vector2Int>
    {
        public override Vector2Int AssembleValue(ObjectModel value)
        {
            return new Vector2Int(value.GetValue<int>("X"), value.GetValue<int>("Y"));
        }

        public override ObjectModel DisassembleValue(Vector2Int value)
            => new ObjectModel(typeof(Vector2Int))
            {
                { "X", value.x },
                { "Y", value.y },
            };
    }

    public class Vector3Assembler : EngineObjectAssemblerBase<Vector3>
    {
        public override Vector3 AssembleValue(ObjectModel value)
        {
            return new Vector3(value.GetValue<float>("X"), value.GetValue<float>("Y"), value.GetValue<float>("Z"));
        }

        public override ObjectModel DisassembleValue(Vector3 value)
            => new ObjectModel(typeof(Vector3))
            {
                { "X", value.x },
                { "Y", value.y },
                { "Z", value.z },
            };
    }

    public class Vector3IntAssembler : EngineObjectAssemblerBase<Vector3Int>
    {
        public override Vector3Int AssembleValue(ObjectModel value)
        {
            return new Vector3Int(value.GetValue<int>("X"), value.GetValue<int>("Y"), value.GetValue<int>("Z"));
        }

        public override ObjectModel DisassembleValue(Vector3Int value)
            => new ObjectModel(typeof(Vector3Int))
            {
                { "X", value.x },
                { "Y", value.y },
                { "Z", value.z },
            };
    }

    public class Vector4Assembler : EngineObjectAssemblerBase<Vector4>
    {
        public override Vector4 AssembleValue(ObjectModel value)
        {
            return new Vector4(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Z"),
                value.GetValue<float>("W"));
        }

        public override ObjectModel DisassembleValue(Vector4 value)
            => new ObjectModel(typeof(Vector4))
            {
                { "X", value.x },
                { "Y", value.y },
                { "Z", value.z },
                { "W", value.w },
            };
    }

    public class QuaternionAssembler : EngineObjectAssemblerBase<Quaternion>
    {
        public override Quaternion AssembleValue(ObjectModel value)
        {
            return new Quaternion(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Z"),
                value.GetValue<float>("W"));
        }

        public override ObjectModel DisassembleValue(Quaternion value)
            => new ObjectModel(typeof (Quaternion))
            {
                { "X", value.x },
                { "Y", value.y },
                { "Z", value.z },
                { "W", value.w },
            };
    }
}
