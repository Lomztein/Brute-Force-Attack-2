using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Assemblers.EngineComponent
{
    public class Rigidbody2DAssembler : EngineComponentAssembler<Rigidbody2D>
    {
        public override void Assemble(ObjectModel model, Rigidbody2D target, AssemblyContext context)
        {
            target.bodyType = model.GetValue<RigidbodyType2D>("BodyType");
        }

        public override ObjectModel Disassemble(Rigidbody2D source, DisassemblyContext context)
        {
            return new ObjectModel(
                new ObjectField ("BodyType", new PrimitiveModel(source.bodyType))
                );
        }
    }
}
