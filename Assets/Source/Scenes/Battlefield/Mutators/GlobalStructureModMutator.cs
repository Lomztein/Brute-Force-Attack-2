using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.StructureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Scenes.Battlefield.Mutators
{
    public class GlobalStructureModMutator : Mutator
    {
        [ModelProperty]
        public GlobalStructureMod Mod;

        public override void Start()
        {
            GlobalStructureModManager.Instance.AddMod(Mod);
        }
    }
}
