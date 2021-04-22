using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    [System.Serializable]
    public abstract class CompletionReward
    {
        public abstract void ApplyReward();
    }
}