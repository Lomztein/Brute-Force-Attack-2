using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement.Filters
{
    public class ColorStructureFilter : IStructureFilter
    {
        [ModelProperty]
        private Colorization.Color[] _applicableColors = new Colorization.Color[0];

        public bool Check(Structure structure)
        {
            if (structure is IColorProvider provider)
            {
                return _applicableColors.Contains(provider.GetColor());
            }
            return false;
        }
    }
}