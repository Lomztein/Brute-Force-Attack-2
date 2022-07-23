using Lomztein.BFA2.Player.Progression;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Research.Rewards
{
    public class UnlockReward : CompletionReward
    {
        [ModelProperty]
        public Unlock[] Unlocks;

        public override void ApplyReward()
        {
            foreach (var unlock in Unlocks)
            {
                // If all prerequisites are researched, unlock instantly.
                if (ResearchController.Instance.ArePrerequisitesResearched(unlock.Prerequisites))
                {
                    Player.Player.Unlocks.SetUnlocked(unlock.Identifier, true);
                }
                else
                {
                    Action<ResearchOption> action = null;
                    action = (x) =>
                    {
                       CheckUnlock(unlock);
                       ResearchController.Instance.OnResearchCompleted -= action;
                    };

                    // Each time something is researched, then check again.
                    ResearchController.Instance.OnResearchCompleted += action;
                }
            }
        }

        void CheckUnlock(Unlock unlock)
        {
            if (ResearchController.Instance.ArePrerequisitesResearched(unlock.Prerequisites))
            {
                Player.Player.Unlocks.SetUnlocked(unlock.Identifier, true);
            }
        }


        [System.Serializable]
        public class Unlock
        {
            [ModelProperty]
            public string Identifier;
            [ModelProperty]
            public Prerequisite[] Prerequisites;
        }
    }
}