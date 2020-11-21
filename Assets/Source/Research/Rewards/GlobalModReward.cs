using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class GlobalModReward : CompletionReward
    {
        [ModelProperty]
        public string TargetGlobalModManager;
        [ModelProperty]
        public IMod Mod;

        public override string Description => Mod.ToString();

        public override void ApplyReward()
        {
        }
    }
}