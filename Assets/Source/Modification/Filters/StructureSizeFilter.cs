using Lomztein.BFA2.Structures;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class StructureSizeFilter : StructureFilter
    {
        private Size _minWidth = Size.Small;
        private Size _minHeight = Size.Small;
        private Size _maxWidth = Size.Huge;
        private Size _maxHeight = Size.Huge;

        public override bool Check(Structure structure)
            => IsWithin(structure.Width, _minWidth, _minHeight) &&
                IsWithin(structure.Height, _minHeight, _maxHeight);

        private bool IsWithin (Size size, Size min, Size max)
        {
            int iSize = (int)size;
            int iMin = (int)min;
            int iMax = (int)max;

            return iSize >= iMin && iSize <= iMax;
        }
    }
}