using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
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
