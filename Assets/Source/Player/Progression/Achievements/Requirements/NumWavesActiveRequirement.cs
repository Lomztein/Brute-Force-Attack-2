using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class NumWavesActiveRequirement : AchievementRequirement
    {
        [ModelProperty]
        public int RequiredAmount;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;

        protected override bool MeetsRequirements()
        {
            int count = RoundController.Instance.ActiveWaves.Count();
            return count == RequiredAmount ||
                (AllowGreater && count > RequiredAmount) ||
                (AllowLesser && count > RequiredAmount);
        }

        public override void End()
        {
            Facade.Battlefield.Enemies.OnWaveFinished -= Enemies_OnWaveFinished;
        }

        public override void Init()
        {
            Facade.Battlefield.Enemies.OnWaveFinished += Enemies_OnWaveFinished;
        }

        private void Enemies_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            CheckRequirements();
        }
    }
}
