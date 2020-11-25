using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public interface IStructureModder
    {
        void AddTo(Structure structure, GlobalStructureMod mod);

        void RemoveFrom(Structure structure, GlobalStructureMod mod);

        bool CanMod(Structure structure);
    }
}
