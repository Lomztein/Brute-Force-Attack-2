using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Scalers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Battlefield.Difficulty
{
    public class EnemyScalerDifficultyAspect : IDifficultyAspect
    {
        public string Name => "Enemy scaling";
        public string Description => "Multipliers for scaling enemy health, armor, and shields.";

        public DifficultyAspectCategory Category => DifficultyAspectCategory.Enemies;

        [DifficultyAspectElement("Health multiplier", null)]
        public float HealthMult = 1f;
        [DifficultyAspectElement("Armor multiplier", null)]
        public float ArmorMult = 1f;
        [DifficultyAspectElement("Shield multiplier", null)]
        public float ShieldMult = 1f;

        public void Apply()
        {
            EnemyScaleController.Instance.AddEnemyScalers(new EnemyScaler(HealthMult, ArmorMult, ShieldMult));
        }
    }
}
