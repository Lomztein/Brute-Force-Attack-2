using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class BoxCollider2DAssembler : EngineComponentAssembler<BoxCollider2D>
    {
        public override void Assemble(ObjectModel model, BoxCollider2D target)
        {
            Vector2Assembler assembler = new Vector2Assembler();

            target.offset = assembler.AssembleValue(model.GetObject("Offset"));
            target.size = assembler.AssembleValue(model.GetObject("Size"));
        }

        public override ObjectModel Disassemble(BoxCollider2D source)
        {
            Vector2Assembler assembler = new Vector2Assembler();

            return new ObjectModel(typeof(BoxCollider2D),
                new ObjectField("Offset", new ComplexPropertyModel(assembler.DisassembleValue(source.offset))),
                new ObjectField("Size", new ComplexPropertyModel (assembler.DisassembleValue(source.size)))
                );
        }
    }
}
