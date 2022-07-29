using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class WaveActiveRequirement : AchievementRequirement
    {
        [ModelProperty]
        public int Wave;

        protected override bool MeetsRequirements()
        {
            return RoundController.Instance.ActiveWaves.Any(x => x.Wave == Wave);
        }

        public override void End()
        {
            Facade.Battlefield.Enemies.OnWaveStarted -= Enemies_OnWaveStarted;
        }

        public override void Init()
        {
            Facade.Battlefield.Enemies.OnWaveStarted += Enemies_OnWaveStarted;
        }

        private void Enemies_OnWaveStarted(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            CheckRequirements();
        }
    }
}
