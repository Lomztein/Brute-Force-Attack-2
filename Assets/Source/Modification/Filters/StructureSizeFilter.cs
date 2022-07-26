using Lomztein.BFA2.Serialization;
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
        [SerializeField, ModelProperty] private Size _minWidth = Size.Small;
        [SerializeField, ModelProperty] private Size _minHeight = Size.Small;
        [SerializeField, ModelProperty] private Size _maxWidth = Size.Huge;
        [SerializeField, ModelProperty] private Size _maxHeight = Size.Huge;

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