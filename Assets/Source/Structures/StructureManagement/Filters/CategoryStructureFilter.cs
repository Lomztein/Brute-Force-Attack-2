using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement.Filters
{
    public class CategoryStructureFilter : IStructureFilter
    {
        [ModelProperty]
        private readonly string _categoryName = "Misc";

        public bool Check(Structure structure) => structure.Category.Name == _categoryName;
    }
}