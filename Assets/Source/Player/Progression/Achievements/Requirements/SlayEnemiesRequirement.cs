using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SlayEnemiesRequirement : AchievementRequirement
    {
        [ModelProperty, OdinSerialize]
        public int TargetEnemies;
        [ModelProperty, OdinSerialize]
        public Colorization.Color[] TargetColors;

        private int _enemiesSlain;
        private BattlefieldController _battlefield;
        private Facade _facade;

        public override bool Binary => false;
        public override float Progression => Mathf.Clamp01(_enemiesSlain / (float)TargetEnemies);
        public override bool Completed => _enemiesSlain / (float)TargetEnemies > 1f;

        public override void End(Facade facade)
        {
            facade.Battlefield.OnAttached -= OnBattlefieldAttached;
            facade.Battlefield.OnDetached -= OnBattlefieldDetached;
        }

        public override void Init(Facade facade)
        {
            facade.Battlefield.OnAttached += OnBattlefieldAttached;
            facade.Battlefield.OnDetached += OnBattlefieldDetached;
            _facade = facade;
        }

        private void OnBattlefieldDetached()
        {
            if (_battlefield)
            {
                _battlefield.RoundController.OnEnemySpawn -= OnEnemySpawn;
                _battlefield = null;
            }
        }

        private void OnBattlefieldAttached()
        {
            _battlefield = _facade.Battlefield.Battlefield;
            _battlefield.RoundController.OnEnemyKill += OnEnemySpawn;
        }

        private void OnEnemySpawn(Enemies.IEnemy obj)
        {
            if (obj is Enemy enemy)
            {
                if (TargetColors.Contains (enemy.Color))
                {
                    _enemiesSlain++;
                    if (_enemiesSlain >= TargetEnemies)
                    {
                        _onCompletedCallback();
                    }
                }
            }
        }
    }
}
