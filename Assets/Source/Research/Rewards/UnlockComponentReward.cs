using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class UnlockComponentReward : CompletionReward
    {
        [ModelProperty]
        public string ComponentIdentifier;

        public override string Description => "Unlocks component: " + ComponentIdentifier;

        public override void ApplyReward()
        {
            Debug.Log("Unlock component " + ComponentIdentifier);
        }
    }
}