using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization.Models;
using UnityEngine;

namespace Lomztein.BFA2.Serialization.Assemblers.EngineObject
{
    public class LayerMaskAssembler : EngineObjectAssemblerBase<LayerMask>
    {
        public override LayerMask AssembleValue(ObjectModel value)
        {
            return value.GetValue<int>("Mask");
        }

        public override ObjectModel DisassembleValue(LayerMask value)
        {
            return new ObjectModel(typeof (LayerMask))
            {
                { "Mask", value.value }
            };
        }
    }
}
