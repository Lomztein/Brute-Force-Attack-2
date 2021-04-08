using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public abstract class CompletionReward
    {
        public abstract string Description { get; }

        public abstract void ApplyReward();
    }
}