using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ReachMasteryModeRequirement : AchievementRequirement
    {
        public override bool Binary => false;
        public override float Progression => (float)_currentMasteryLevel / TargetMasteryLevel;
        public override bool RequirementsMet => _currentMasteryLevel == TargetMasteryLevel;

        private int _currentMasteryLevel;
        [ModelProperty]
        public int TargetMasteryLevel;

        public override void End()
        {
            Facade.Battlefield.Mastery.OnMasteryModeLevelChanged -= Mastery_OnMasteryModeLevelChanged;
        }

        public override void Init()
        {
            Facade.Battlefield.Mastery.OnMasteryModeLevelChanged += Mastery_OnMasteryModeLevelChanged;
        }

        private void Mastery_OnMasteryModeLevelChanged(int arg1, object arg2)
        {
            _currentMasteryLevel = arg1;
        }
    }
}
