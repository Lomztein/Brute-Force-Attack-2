using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public class GenericStructureModder : IStructureModder
    {
        public void AddTo(Structure structure, GlobalStructureMod mod)
        {
            mod.TryApply(structure);
        }

        public void RemoveFrom(Structure structure, GlobalStructureMod mod)
        {
            mod.RemoveFrom(structure);
        }

        public bool CanMod(Structure structure) => true;
    }
}