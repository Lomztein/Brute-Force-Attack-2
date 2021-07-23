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
        [ModelProperty]
        private IMod _mod;
        [ModelProperty]
        private IStructureFilter[] _filters;

        public GlobalStructureMod ()
        {
            _filters = new IStructureFilter[0];
        }

        public GlobalStructureMod(IMod mod, params IStructureFilter[] filters)
        {
            _mod = mod;
            _filters = filters;
        }

        public bool TryApply (Structure structure)
        {
            if (_filters.All(x => x.Check(structure)) && _mod.IsCompatableWith(structure))
            {
                structure.Mods.AddMod(_mod.DeepClone());
                return true;
            }
            return false;
        }

        public bool TryRemove(Structure structure)
        {
            if (_filters.All(x => x.Check(structure)))
            {
                structure.Mods.RemoveMod(_mod.Identifier);
                return true;
            }
            return false;
        }
    }
}
