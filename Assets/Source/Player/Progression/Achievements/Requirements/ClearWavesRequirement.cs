using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ClearWavesRequirement : AchievementRequirement
    {
        public override bool Binary => false;

        private int _amount;
        [ModelProperty]
        public int Target;
        [ModelProperty]
        public bool CrossGames;

        public override float Progression => (float)_amount / Target;
        public override bool Completed => _amount >= Target;

        public override void End()
        {
            Facade.Battlefield.Enemies.OnWaveCleared -= OnWaveCleared;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneLoaded -= Battlefield_OnSceneLoaded;
            }
        }

        public override void Init()
        {
            Facade.Battlefield.Enemies.OnWaveCleared += OnWaveCleared;
            if (!CrossGames) // If not Cross Games, reset when loading the new battlefield. Might cause issues with save-games?
            {
                Facade.Battlefield.OnSceneLoaded += Battlefield_OnSceneLoaded;
            }
        }

        private void Battlefield_OnSceneLoaded()
        {
            _amount = 0;
            _onProgressedCallback();
        }

        private void OnWaveCleared(int index, Enemies.Waves.IWave wave)
        {
            if (index > _amount)
            {
                _onProgressedCallback();
                _amount = index;
                if (_amount >= Target)
                {
                    _onCompletedCallback();
                }
            }
        }

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_amount);
        }

        public override void DeserializeProgress(ValueModel source)
        {
            _amount = (source as PrimitiveModel).ToObject<int>();
        }
    }
}
