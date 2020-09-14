using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Serialization.Models.Property;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public override Rect AssembleValue(IObjectModel value)
        {
            return new Rect(
                value.GetValue<float>("X"),
                value.GetValue<float>("Y"),
                value.GetValue<float>("Width"),
                value.GetValue<float>("Height")
                );
        }

        public override IObjectModel DisassembleValue(Rect value)
        {
            return new ObjectModel(typeof(Rect),
                new ObjectField ("X", new ValuePropertyModel(value.x)),
                new ObjectField ("Y", new ValuePropertyModel(value.y)),
                new ObjectField ("Width", new ValuePropertyModel(value.width)),
                new ObjectField ("Height", new ValuePropertyModel(value.height))
                );
        }
    }
}
