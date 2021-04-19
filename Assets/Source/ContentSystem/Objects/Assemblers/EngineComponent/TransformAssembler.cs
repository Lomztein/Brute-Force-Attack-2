using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent
{
    public class TransformAssembler : EngineComponentAssembler<Transform>
    {
        public override void Assemble(ObjectModel model, Transform target, AssemblyContext context)
        {
            Vector3Assembler assembler = new Vector3Assembler();

            Vector3 position = assembler.AssembleValue(model.GetObject("Position"), context);
            Vector3 rotation = assembler.AssembleValue(model.GetObject("Rotation"), context);
            Vector3 scale = assembler.AssembleValue(model.GetObject("Scale"), context);

            target.localPosition = position;
            target.localRotation = Quaternion.Euler (rotation);
            target.localScale = scale;
        }

        public override ObjectModel Disassemble(Transform source, DisassemblyContext context)
        {
            Vector3Assembler assembler = new Vector3Assembler();

            return new ObjectModel(
                new ObjectField("Position", assembler.DisassembleValue(source.localPosition, context)),
                new ObjectField("Rotation", assembler.DisassembleValue(source.localRotation.eulerAngles, context)),
                new ObjectField("Scale", assembler.DisassembleValue(source.localScale, context))
                );
        }
    }
}
