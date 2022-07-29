using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class DifficultyRequirement : AchievementRequirement
    {
        [ModelProperty]
        public string[] ApplicableDifficultyIdentifiers;

        protected override bool MeetsRequirements()
            => ApplicableDifficultyIdentifiers.Contains(Battlefield.BattlefieldController.Instance.CurrentSettings.Difficulty.Identifier);

        public override void End()
        {
            Facade.Battlefield.OnBattlefieldInitialized -= OnBattlefieldInitialized;
        }

        public override void Init()
        {
            Facade.Battlefield.OnBattlefieldInitialized += OnBattlefieldInitialized;
        }

        private void OnBattlefieldInitialized()
        {
            CheckRequirements();
        }
    }
}
