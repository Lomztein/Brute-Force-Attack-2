using Lomztein.BFA2.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public class EnemySpawnDifficultyAspect : IDifficultyAspect
    {
        public string Name => "Enemy spawns";
        public string Description => "Multipliers that scale enemy spawn amount and frequency.";
        public DifficultyAspectCategory Category => DifficultyAspectCategory.Enemies;

        [DifficultyAspectElement("Amount multiplier", null)]
        public float AmountMultiplier = 1f;
        [DifficultyAspectElement("Frequency multiplier", null)]
        public float FrequencyMultiplier = 1f;

        public void Apply()
        {
            RoundController.Instance.EnemyAmountMultiplier = AmountMultiplier;
            RoundController.Instance.SpawnFrequencyMultiplier = FrequencyMultiplier;
        }
    }
}
