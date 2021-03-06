﻿using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class UnlockReward : CompletionReward
    {
        [ModelProperty]
        public string Identifier;

        public override void ApplyReward()
        {
            Player.Player.Unlocks.SetUnlocked(Identifier, true);
        }
    }
}