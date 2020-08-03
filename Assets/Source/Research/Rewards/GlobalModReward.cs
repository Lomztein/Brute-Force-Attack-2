using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class GlobalModReward : CompletionReward
    {
        public override void ApplyReward()
        {
            Debug.Log("Global mod reward!");
        }
    }
}