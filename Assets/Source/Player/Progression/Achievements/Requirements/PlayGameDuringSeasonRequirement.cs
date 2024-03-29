using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class PlayGameDuringMonth : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => RequirementsMet ? 1f : 0f;
        public override bool RequirementsMet => DateTime.Now.Month == MonthIndex;

        [ModelProperty]
        public int MonthIndex; 

        public override void End()
        {
        }

        public override void Init()
        {
            if (DateTime.Now.Month == MonthIndex)
            {
                CheckRequirements();
            }
        }
    }
}
