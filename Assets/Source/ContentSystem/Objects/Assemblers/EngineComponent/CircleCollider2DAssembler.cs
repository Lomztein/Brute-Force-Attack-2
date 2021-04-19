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
    public class CircleCollider2DAssembler : EngineComponentAssembler<CircleCollider2D>
    {
        public override void Assemble(ObjectModel model, CircleCollider2D target, AssemblyContext context)
        {
            Vector2Assembler vs = new Vector2Assembler();

            target.radius = model.GetValue<float>("Radius");
            target.offset = vs.AssembleValue(model.GetObject("Offset"), context);
            target.isTrigger = model.GetValue<bool>("IsTrigger");
            target.usedByEffector = model.GetValue<bool>("UsedByEffector");
        }

        public override ObjectModel Disassemble(CircleCollider2D source, DisassemblyContext context)
        {
            Vector2Assembler vs = new Vector2Assembler();

            return new ObjectModel(
                new ObjectField ("Radius", new PrimitiveModel(source.radius)),
                new ObjectField("Offset", vs.DisassembleValue(source.offset, context)),
                new ObjectField("IsTrigger", new PrimitiveModel(source.isTrigger)),
                new ObjectField("UsedByEffector", new PrimitiveModel(source.usedByEffector)));
        }
    }
}
