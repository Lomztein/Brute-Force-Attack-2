using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    [System.Serializable]
    public class StructureClassFilter : StructureFilter
    {
        [ModelProperty]
        private string _typeName = "";

        public override bool Check(Structure structure)
            => structure.GetType().FullName == _typeName;
    }
}