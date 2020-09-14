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
    public class TransformAssembler : EngineComponentAssembler<Transform>
    {
        public override void Assemble(IObjectModel model, Transform target)
        {
            Vector3Assembler assembler = new Vector3Assembler();

            Vector3 position = assembler.AssembleValue(model.GetObject("Position"));
            Vector3 rotation = assembler.AssembleValue(model.GetObject("Rotation"));
            Vector3 scale = assembler.AssembleValue(model.GetObject("Scale"));

            target.localPosition = position;
            target.localRotation = Quaternion.Euler (rotation);
            target.localScale = scale;
        }

        public override IObjectModel Disassemble(Transform source)
        {
            Vector3Assembler assembler = new Vector3Assembler();

            return new ObjectModel(typeof(Transform),
                new ObjectField("Position", new ObjectPropertyModel(assembler.DissasembleValue(source.localPosition))),
                new ObjectField("Rotation", new ObjectPropertyModel(assembler.DissasembleValue(source.localRotation.eulerAngles))),
                new ObjectField("Scale", new ObjectPropertyModel(assembler.DissasembleValue(source.localScale)))
                );
        }
    }
}
