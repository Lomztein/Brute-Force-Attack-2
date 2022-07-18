using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class UnlockReward : CompletionReward
    {
        [ModelProperty]
        public string[] Unlocks;

        public override void ApplyReward()
        {
            foreach (var unlock in Unlocks)
            {
                Player.Player.Unlocks.SetUnlocked(unlock, true);
            }
        }
    }
}