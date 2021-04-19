using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class RectAssembler : EngineObjectAssemblerBase<Rect>
    {
        public override Rect AssembleValue(ObjectModel value, AssemblyContext context)
        {
            return new Rect(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Width"),
                value.GetValue<float>("Height")
                );
        }

        public override ObjectModel DisassembleValue(Rect value, DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField ("X", new PrimitiveModel(value.x)),
                new ObjectField ("Y", new PrimitiveModel(value.y)),
                new ObjectField ("Width", new PrimitiveModel(value.width)),
                new ObjectField ("Height", new PrimitiveModel(value.height))
                );
        }
    }
}
