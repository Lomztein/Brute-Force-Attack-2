using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class GradientAssembler : EngineObjectAssemblerBase<Gradient>
    {
        public override Gradient AssembleValue(ObjectModel value)
        {
            ColorAssembler assembler = new ColorAssembler();
            Gradient gradient = new Gradient();

            gradient.mode = value.GetValue<GradientMode>("Mode");
            gradient.colorKeys = value.GetArray("ColorKeys").Select(x => new GradientColorKey((Color)assembler.Assemble((x as ComplexPropertyModel).Model.GetObject("Color")), (x as ComplexPropertyModel).Model.GetValue<float>("Time"))).ToArray();
            gradient.alphaKeys = value.GetArray("AlphaKeys").Select(x => new GradientAlphaKey((x as ComplexPropertyModel).Model.GetValue<float>("Alpha"), (x as ComplexPropertyModel).Model.GetValue<float>("Time"))).ToArray();

            return gradient;
        }

        public override ObjectModel DisassembleValue(Gradient value)
        {
            ColorAssembler assembler = new ColorAssembler();

            return new ObjectModel(typeof(Gradient),
                new ObjectField("ColorKeys", new ArrayPropertyModel(typeof(GradientColorKey[]),
                    value.colorKeys.Select(x => new ComplexPropertyModel(new ObjectModel(typeof(GradientColorKey),
                        new ObjectField("Time",  new PrimitivePropertyModel(x.time)),
                        new ObjectField ("Color", new ComplexPropertyModel(assembler.DisassembleValue (x.color)))))))),

                new ObjectField ("AlphaKeys", new ArrayPropertyModel(typeof(GradientAlphaKey[]), 
                    value.alphaKeys.Select(x => new ComplexPropertyModel(new ObjectModel(typeof (GradientAlphaKey),
                        new ObjectField ("Time", new PrimitivePropertyModel(x.time)), 
                        new ObjectField ("Alpha", new PrimitivePropertyModel(x.alpha))))))),

                new ObjectField ("Mode", new PrimitivePropertyModel((int)value.mode))
            );
        }
    }
}
