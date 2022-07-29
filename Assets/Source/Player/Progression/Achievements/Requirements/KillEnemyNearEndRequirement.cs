using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class KillEnemyNearEndRequirement : AchievementRequirement
    {
        public override bool Binary => false;
        public override float Progression => (float)_count / RequiredCount;

        [ModelProperty]
        public float DistanceThreshold;
        [ModelProperty]
        public string[] ApplicableEnemyIdentifiers;
        [ModelProperty]
        public int RequiredCount;
        [ModelProperty]
        public bool CrossGames;

        private int _count;

        protected override bool MeetsRequirements()
        {
            return _count >= RequiredCount;
        }

        public override void End()
        {
            Facade.Battlefield.Enemies.OnEnemyKilled -= Enemies_OnEnemyKilled;
            Facade.Battlefield.OnSceneUnloaded -= Battlefield_OnSceneUnloaded;
        }

        public override void Init()
        {
            Facade.Battlefield.Enemies.OnEnemyKilled += Enemies_OnEnemyKilled;
            Facade.Battlefield.OnSceneUnloaded += Battlefield_OnSceneUnloaded;
        }

        private void Battlefield_OnSceneUnloaded()
        {
            if (!CrossGames)
            {
                _count = 0;
            }
        }

        private void Enemies_OnEnemyKilled(Enemies.Enemy obj)
        {
            if (!ApplicableEnemyIdentifiers.Any() || ApplicableEnemyIdentifiers.Any(x => x.StartsWith(obj.Identifier)))
            {
                int length = obj.Path.Length;
                int index = obj.PathIndex;
                if (length - index < length)
                {
                    _count++;
                }
            }
        }

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_count);
        }

        public override void DeserializeProgress(ValueModel source)
        {
            _count = (source as PrimitiveModel).ToObject<int>();
        }
    }
}
