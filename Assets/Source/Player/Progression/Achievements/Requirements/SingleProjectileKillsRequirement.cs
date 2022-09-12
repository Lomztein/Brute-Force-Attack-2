using Lomztein.BFA2.Weaponary.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SingleProjectileKillsRequirement : AchievementRequirement
    {
        public float RequiredKills;
        private float _maxKills;

        public override void End()
        {
            Facade.Battlefield.Enemies.OnEnemyKilled += Enemies_OnEnemyKilled;
        }

        private void Enemies_OnEnemyKilled(Enemies.Enemy obj)
        {
            if (obj.LastDamageTaken.TryGetSource(out Projectile projectile))
            {
                _maxKills = Mathf.Max(_maxKills, projectile.Kills);
                if (MeetsRequirements())
                {
                    CheckRequirements();
                }
            }
        }

        protected override bool MeetsRequirements()
            => _maxKills >= RequiredKills;

        public override void Init()
        {
        }
    }
}
