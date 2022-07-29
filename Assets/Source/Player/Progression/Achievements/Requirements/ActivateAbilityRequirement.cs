using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ActivateAbilityRequirement : AchievementRequirement
    {
        [ModelProperty]
        public string[] ApplicableAbilities;
        [ModelProperty]
        public bool ResetOnNoActiveWaves;
        [ModelProperty]
        public int RequiredCount;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;

        private int _activationCount;

        protected override bool MeetsRequirements()
        {
            return _activationCount == RequiredCount || // I should really turn this into a utility function or something.
                (AllowGreater && _activationCount > RequiredCount) ||
                (AllowLesser && _activationCount < RequiredCount);
        }

        public override void End()
        {
            Facade.Battlefield.Abilities.OnAbilityActivated -= Abilities_OnAbilityActivated;
            Facade.Battlefield.Enemies.OnWaveFinished -= Enemies_OnWaveFinished;
        }

        public override void Init()
        {
            Facade.Battlefield.Abilities.OnAbilityActivated += Abilities_OnAbilityActivated;
            Facade.Battlefield.Enemies.OnWaveFinished += Enemies_OnWaveFinished;
        }

        private void Enemies_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            if (ResetOnNoActiveWaves && RoundController.Instance.ActiveWaves.Length == 0)
            {
                _activationCount = 0;
            }
        }

        private void Abilities_OnAbilityActivated(Abilities.Ability arg1, object arg2)
        {
            if (ApplicableAbilities.Any(x => arg1.Identifier.StartsWith(x)))
            {
                _activationCount++;
                CheckRequirements();
            }
        }
    }
}
