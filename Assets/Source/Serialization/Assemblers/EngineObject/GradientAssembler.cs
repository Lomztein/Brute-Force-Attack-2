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
    public class GradientAssembler : EngineObjectAssemblerBase<Gradient>
    {
        public override Gradient AssembleValue(ObjectModel value, AssemblyContext context)
        {
            ColorAssembler assembler = new ColorAssembler();
            Gradient gradient = new Gradient();

            gradient.mode = value.GetValue<GradientMode>("Mode");
            gradient.colorKeys = value.GetArray("ColorKeys").Select(x => new GradientColorKey((Color)assembler.Assemble((x as ObjectModel).GetObject("Color"), context), (x as ObjectModel).GetValue<float>("Time"))).ToArray();
            gradient.alphaKeys = value.GetArray("AlphaKeys").Select(x => new GradientAlphaKey((x as ObjectModel).GetValue<float>("Alpha"), (x as ObjectModel).GetValue<float>("Time"))).ToArray();

            return gradient;
        }

        public override ObjectModel DisassembleValue(Gradient value, DisassemblyContext context)
        {
            ColorAssembler assembler = new ColorAssembler();

            return new ObjectModel(
                new ObjectField("ColorKeys", new ArrayModel(
                    value.colorKeys.Select(x => new ObjectModel(
                        new ObjectField("Time",  new PrimitiveModel(x.time)),
                        new ObjectField("Color", assembler.DisassembleValue (x.color, context)))))),

                new ObjectField("AlphaKeys", new ArrayModel(
                    value.alphaKeys.Select(x => new ObjectModel(
                        new ObjectField("Time", new PrimitiveModel(x.time)), 
                        new ObjectField("Alpha", new PrimitiveModel(x.alpha)))))),

                new ObjectField ("Mode", new PrimitiveModel((int)value.mode))
            );
        }
    }
}
