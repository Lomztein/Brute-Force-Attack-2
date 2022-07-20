using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class PlayerModResearchReward : CompletionReward
    {
        [ModelAssetReference]
        public Mod Mod;

        public override void ApplyReward()
        {
            Player.Player.Mods.AddMod(Mod);
        }
    }
}
