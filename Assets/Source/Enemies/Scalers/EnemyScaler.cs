using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Enemies.Scalers
{
    public class EnemyScaler : EnemyScalerBase<Enemy>
    {
        public float HealthMult = 1f;
        public float ArmorMult = 1f;
        public float ShieldMult = 1f;

        public EnemyScaler() { }

        public EnemyScaler (float health, float armor, float shield)
        {
            HealthMult = health;
            ArmorMult = armor;
            ShieldMult = shield;
        }

        public override void Scale (Enemy enemy)
        {
            enemy.MaxHealth *= HealthMult;
            enemy.Health *= HealthMult;
            enemy.Armor *= ArmorMult;
            enemy.Shields = ShieldMult;
        }
    }
}
