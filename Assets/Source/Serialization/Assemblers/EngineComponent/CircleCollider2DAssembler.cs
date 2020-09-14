using Lomztein.BFA2.Serialization.Assemblers.EngineObject;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class CircleCollider2DAssembler : EngineComponentAssembler<CircleCollider2D>
    {
        public override void Assemble(IObjectModel model, CircleCollider2D target)
        {
            Vector2Assembler vs = new Vector2Assembler();

            target.radius = model.GetValue<float>("Radius");
            target.offset = vs.AssembleValue(model.GetObject("Offset"));
            target.isTrigger = model.GetValue<bool>("IsTrigger");
            target.usedByEffector = model.GetValue<bool>("UsedByEffector");
        }

        public override IObjectModel Disassemble(CircleCollider2D source)
        {
            Vector2Assembler vs = new Vector2Assembler();

            return new ObjectModel(
                typeof (CircleCollider2D),
                new ObjectField ("Radius", new ValuePropertyModel(source.radius)),
                new ObjectField("Offset", new ObjectPropertyModel(vs.DissasembleValue(source.offset))),
                new ObjectField("IsTrigger", new ValuePropertyModel(source.isTrigger)),
                new ObjectField("UsedByEffector", new ValuePropertyModel(source.usedByEffector)));
        }
    }
}
