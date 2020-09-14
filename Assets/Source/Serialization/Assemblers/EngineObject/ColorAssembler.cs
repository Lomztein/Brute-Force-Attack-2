﻿using System;
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
    public class ColorAssembler : EngineObjectAssemblerBase<Color>
    {
        public override Color AssembleValue(IObjectModel value)
        {
            return new Color(value.GetValue<float>("Red"), value.GetValue<float>("Green"), value.GetValue<float>("Blue"), value.GetValue<float>("Alpha"));
        }

        public override IObjectModel DisassembleValue(Color value)
        {
            return new ObjectModel(typeof(Color),
                new ObjectField ("Red", new ValuePropertyModel(value.r)),
                new ObjectField ("Green", new ValuePropertyModel(value.g)),
                new ObjectField ("Blue", new ValuePropertyModel(value.b)),
                new ObjectField ("Alpha", new ValuePropertyModel(value.a))
                );
        }
    }
}
