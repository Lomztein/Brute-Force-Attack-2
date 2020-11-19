using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class ColorAssembler : EngineObjectAssemblerBase<Color>
    {
        public override Color AssembleValue(ObjectModel value)
        {
            return new Color(value.GetValue<float>("Red"), value.GetValue<float>("Green"), value.GetValue<float>("Blue"), value.GetValue<float>("Alpha"));
        }

        public override ObjectModel DisassembleValue(Color value)
        {
            return new ObjectModel(
                new ObjectField ("Red", new PrimitiveModel(value.r)),
                new ObjectField("Green", new PrimitiveModel(value.g)),
                new ObjectField("Blue", new PrimitiveModel(value.b)),
                new ObjectField("Alpha", new PrimitiveModel(value.a))
                );
        }
    }
}
