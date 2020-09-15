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
        public override Vector2 AssembleValue(IObjectModel value)
        {
            return new Vector2(value.GetValue<float>("X"), value.GetValue<float>("Y"));
        }

        public override IObjectModel DisassembleValue(Vector2 value)
            => new ObjectModel(typeof(Vector2),
                new ObjectField("X", new PrimitivePropertyModel(value.x)), 
                new ObjectField("Y", new PrimitivePropertyModel(value.y)));
    }

    public class Vector2IntAssembler : EngineObjectAssemblerBase<Vector2Int>
    {
        public override Vector2Int AssembleValue(IObjectModel value)
        {
            return new Vector2Int(value.GetValue<int>("X"), value.GetValue<int>("Y"));
        }

        public override IObjectModel DisassembleValue(Vector2Int value)
            => new ObjectModel(typeof(Vector2), 
                new ObjectField ("X", new PrimitivePropertyModel(value.x)), 
                new ObjectField ("Y", new PrimitivePropertyModel(value.y)));
    }

    public class Vector3Assembler : EngineObjectAssemblerBase<Vector3>
    {
        public override Vector3 AssembleValue(IObjectModel value)
        {
            return new Vector3(value.GetValue<float>("X"), value.GetValue<float>("Y"), value.GetValue<float>("Z"));
        }

        public override IObjectModel DisassembleValue(Vector3 value)
            => new ObjectModel(typeof(Vector3),
                new ObjectField("X", new PrimitivePropertyModel(value.x)),
                new ObjectField("Y", new PrimitivePropertyModel(value.y)),
                new ObjectField("Z", new PrimitivePropertyModel(value.z)));
    }

    public class Vector3IntAssembler : EngineObjectAssemblerBase<Vector3Int>
    {
        public override Vector3Int AssembleValue(IObjectModel value)
        {
            return new Vector3Int(value.GetValue<int>("X"), value.GetValue<int>("Y"), value.GetValue<int>("Z"));
        }

        public override IObjectModel DisassembleValue(Vector3Int value)
            => new ObjectModel(typeof(Vector3Int),
                new ObjectField("X", new PrimitivePropertyModel(value.x)), 
                new ObjectField("Y", new PrimitivePropertyModel(value.y)),
                new ObjectField("Z", new PrimitivePropertyModel(value.z)));
    }

    public class Vector4Assembler : EngineObjectAssemblerBase<Vector4>
    {
        public override Vector4 AssembleValue(IObjectModel value)
        {
            return new Vector4(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Z"),
                value.GetValue<float>("W"));
        }

        public override IObjectModel DisassembleValue(Vector4 value)
            => new ObjectModel(typeof(Vector4),
                new ObjectField ("X", new PrimitivePropertyModel(value.x)), 
                new ObjectField ("Y", new PrimitivePropertyModel(value.y)), 
                new ObjectField ("Z", new PrimitivePropertyModel(value.z)),
                new ObjectField ("W", new PrimitivePropertyModel(value.w)));
    }

    public class QuaternionAssembler : EngineObjectAssemblerBase<Quaternion>
    {
        public override Quaternion AssembleValue(IObjectModel value)
        {
            return new Quaternion(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Z"),
                value.GetValue<float>("W"));
        }

        public override IObjectModel DisassembleValue(Quaternion value)
            => new ObjectModel(typeof(Quaternion),
                new ObjectField("X", new PrimitivePropertyModel(value.x)),
                new ObjectField("Y", new PrimitivePropertyModel(value.y)),
                new ObjectField("Z", new PrimitivePropertyModel(value.z)),
                new ObjectField("W", new PrimitivePropertyModel(value.w)));
    }
}
