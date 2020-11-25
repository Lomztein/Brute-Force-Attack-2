using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement.Filters
{
    public class TypeStructureFilter : IStructureFilter
    {
        [ModelProperty]
        private string _typeName = "";

        public bool Check(Structure structure)
            => structure.GetType().FullName == _typeName;
    }
}