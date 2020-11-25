using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.StructureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Plugins
{
    public class GlobalStructureModMutator : Mutator
    {
        [ModelProperty]
        private GlobalStructureMod _mod;

        private void Start()
        {
            GlobalStructureModManager.Instance.AddMod(_mod);
        }
    }
}
