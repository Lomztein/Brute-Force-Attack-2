using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineComponent
{
    public class Rigidbody2DAssembler : EngineComponentAssembler<Rigidbody2D>
    {
        public override void Assemble(ObjectModel model, Rigidbody2D target)
        {
            target.bodyType = model.GetValue<RigidbodyType2D>("BodyType");
        }

        public override ObjectModel Disassemble(Rigidbody2D source)
        {
            return new ObjectModel(typeof(Rigidbody2D),
                new ObjectField ("BodyType", new PrimitivePropertyModel(source.bodyType))
                );
        }
    }
}
