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

        public GlobalStructureMod(Mod mod)
        {
            Mod = mod;
        }

        public bool TryApply (Structure structure)
        {
            if (Mod.CanMod(structure))
            {
                structure.Mods.AddMod(UnityEngine.Object.Instantiate(Mod));
                return true;
            }
            return false;
        }

        public void RemoveFrom(Structure structure)
        {
            structure.Mods.RemoveMod(Mod.Identifier);
        }
    }
}
