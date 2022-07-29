using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class AllResearchCompletedRequirement : AchievementRequirement
    {
        protected override bool MeetsRequirements()
        {
            return Facade.Battlefield.Research.Controller.GetCompleted().Length == Facade.Battlefield.Research.Controller.GetAll().Length;
        }

        public override void End()
        {
            Facade.Battlefield.Research.OnResearchCompleted -= Research_OnResearchCompleted;
        }

        public override void Init()
        {
            Facade.Battlefield.Research.OnResearchCompleted += Research_OnResearchCompleted;
        }

        private void Research_OnResearchCompleted(BFA2.Research.ResearchOption obj)
        {
            CheckRequirements();
        }
    }
}
