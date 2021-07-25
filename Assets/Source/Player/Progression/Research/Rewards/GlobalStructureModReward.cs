using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.StructureManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class GlobalStructureModReward : CompletionReward
    {
        [ModelProperty]
        public GlobalStructureMod Mod;

        public override void ApplyReward()
        {
            GlobalStructureModManager.Instance.AddMod(Mod);
        }
    }
}