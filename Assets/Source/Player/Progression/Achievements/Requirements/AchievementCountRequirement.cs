using Lomztein.BFA2.Player.Profile;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class AchievementCountRequirement : AchievementRequirement
    {
        [ModelProperty]
        public int RequiredCount;

        protected override bool MeetsRequirements()
        {
            return AchievementManager.Instance.Achievements.Count(x => ProfileManager.CurrentProfile.GetAchievementStatus(x.Identifier).IsCompleted) == RequiredCount;
        }

        public override void End()
        {
            AchievementManager.Instance.OnAchievementCompleted -= Instance_OnAchievementCompleted;
        }

        public override void Init()
        {
            AchievementManager.Instance.OnAchievementCompleted += Instance_OnAchievementCompleted;
        }

        private void Instance_OnAchievementCompleted(Achievement obj)
        {
            CheckRequirements();
        }
    }
}
