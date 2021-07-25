using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.StructureManagement
{
    public class GlobalStructureMod
    {
        [ModelAssetReference]
        public Mod Mod;
        [ModelProperty]
        private IStructureFilter[] Filters;

        public GlobalStructureMod ()
        {
            Filters = new IStructureFilter[0];
        }

        public GlobalStructureMod(Mod mod, params IStructureFilter[] filters)
        {
            Mod = mod;
            Filters = filters;
        }

        public bool TryApply (Structure structure)
        {
            if (Filters.All(x => x.Check(structure)) && Mod.CanMod(structure))
            {
                structure.Mods.AddMod(UnityEngine.Object.Instantiate(Mod));
                return true;
            }
            return false;
        }

        public bool TryRemove(Structure structure)
        {
            if (Filters.All(x => x.Check(structure)))
            {
                structure.Mods.RemoveMod(Mod.Identifier);
                return true;
            }
            return false;
        }
    }
}
