using Lomztein.BFA2.Structures.Turrets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public class AssemblyStructureModder : IStructureModder
    {
        public void AddTo(Structure structure, GlobalStructureMod mod)
        {
            mod.TryApply(structure);
            TurretAssembly assembly = structure as TurretAssembly;
            foreach (var component in assembly.GetComponents())
            {
                mod.TryApply(component);
            }
        }

        public void RemoveFrom(Structure structure, GlobalStructureMod mod)
        {
            mod.TryRemove(structure);
            TurretAssembly assembly = structure as TurretAssembly;
            foreach (var component in assembly.GetComponents())
            {
                mod.TryRemove(component);
            }
        }

        public bool CanMod(Structure structure) => structure is TurretAssembly;
    }
}