using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    [Serializable]
    public class SlayEnemiesRequirement : AchievementRequirement
    {
        [ModelProperty]
        public int TargetEnemies;
        [ModelProperty]
        public Colorization.Color[] TargetColors;

        private int _enemiesSlain;
        private Facade _facade;

        public override bool Binary => false;
        public override float Progression => Mathf.Clamp01(_enemiesSlain / (float)TargetEnemies);
        public override bool Completed => _enemiesSlain / (float)TargetEnemies > 1f;

        public override void End(Facade facade)
        {
            if (_facade.Battlefield.Active)
            {
                _facade.Battlefield.Battlefield.RoundController.OnEnemyKill -= OnEnemySpawn;
            }
        }

        public override void Init(Facade facade)
        {
            _facade = facade;
            if (_facade.Battlefield.Active)
            {
                _facade.Battlefield.Battlefield.RoundController.OnEnemyKill += OnEnemySpawn;
            }
        }

        private void OnEnemySpawn(IEnemy obj)
        {
            if (obj is Enemy enemy)
            {
                if (TargetColors.Contains (enemy.Color))
                {
                    _enemiesSlain++;
                    _onProgressedCallback();
                    if (_enemiesSlain >= TargetEnemies)
                    {
                        _onCompletedCallback();
                    }
                }
            }
        }

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_enemiesSlain);
        }

        public override void DeserializeProgress (ValueModel model)
        {
            if (!ValueModel.IsNull(model))
            {
                _enemiesSlain = (model as PrimitiveModel).ToObject<int>();
            }
        }
    }
}
