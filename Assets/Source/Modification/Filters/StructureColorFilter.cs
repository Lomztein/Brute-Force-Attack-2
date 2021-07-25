using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class StructureColorFilter : StructureFilter
    {
        [ModelProperty]
        private Colorization.Color[] _applicableColors = new Colorization.Color[0];

        public override bool Check(Structure structure)
        {
            if (structure is IColorProvider provider)
            {
                return _applicableColors.Contains(provider.GetColor());
            }
            return false;
        }
    }
}