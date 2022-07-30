using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class CategoryStructureFilter : StructureFilter
    {
        [ModelProperty]
        public string CategoryName = "Misc";

        public override bool Check(Structure structure) => (!structure.Category && string.IsNullOrEmpty(CategoryName)) || (structure.Category && structure.Category.Name == CategoryName);
    }
}