using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Assets.Source.Enemies.Special
{
    public class EnemyHealer : MonoBehaviour
    {
        private Enemy _enemy;

        [ModelProperty]
        public double HealRatePercentage;
        [ModelProperty]
        public double MaxHealPercentage;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void FixedUpdate()
        {
            double max = _enemy.MaxHealth * MaxHealPercentage;
            if (_enemy.Health < max)
            {
                double rate = _enemy.MaxHealth * HealRatePercentage;
                _enemy.Heal(rate * (double)Time.fixedDeltaTime);
            }
        }
    }
}
