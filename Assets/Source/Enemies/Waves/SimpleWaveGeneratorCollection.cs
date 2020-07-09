using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization.Models.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class SimpleWaveGeneratorCollection : IWaveCollection
    {
        // TODO: Create a common access point for these paths to allow for easier changes.
        private const string ENEMY_CONTENT_PATH = "*/Enemies";

        private IContentCachedPrefab[] _enemies;
        private System.Random _random;

        public GameObject Spawner;

        public int Seed;
        public float StartingCredits;
        public float Coeffecient;

        public float StartingFrequency;
        public float FrequencyCoeffecient;

        public float StartingEarnedFromKills;
        public float EarnedFromKillsCoeffecient;

        public float StartingCompletionReward;
        public float CompletionRewardCoeffecient;

        public int MaxSpawnDelayWave;

        private IContentCachedPrefab[] GetEnemies ()
        {
            if (_enemies == null)
            {
                _enemies = Content.Content.GetAll(ENEMY_CONTENT_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _enemies;
        }

        private System.Random GetRandom ()
        {
            if (_random == null)
            {
                System.Random seedRandom = new System.Random();
                Seed = seedRandom.Next(int.MinValue, int.MaxValue);

                _random = new System.Random(Seed);
            }
            return _random;
        }

        private (IContentCachedPrefab enemy, int amount) GetRandomEnemy (float credits)
        {
            var enemies = GetEnemies();
            IContentCachedPrefab enemy = enemies[GetRandom().Next(0, enemies.Length)];
            IEnemy component = enemy.GetCache().GetComponent<IEnemy>();
            return (enemy, Mathf.RoundToInt(credits / component.DifficultyValue));
        }

        private float GetSpawnFrequency(int wave) => StartingFrequency * Mathf.Pow(FrequencyCoeffecient, wave);

        private float GetAvailableCredits(int wave) => StartingCredits * Mathf.Pow(Coeffecient, wave);

        private float GetEarnedFromKills (int wave) => StartingEarnedFromKills * Mathf.Pow(EarnedFromKillsCoeffecient, wave);

        private float GetCompletionReward (int wave) => StartingCompletionReward * Mathf.Pow(CompletionRewardCoeffecient, wave);

        public IWave GetWave(int index)
        {
            float credits = GetAvailableCredits(index);

            (IContentCachedPrefab enemy, int amount) = GetRandomEnemy(credits);
            float frequency = GetSpawnFrequency(index) / enemy.GetCache().GetComponent<IEnemy>().DifficultyValue;

            return new SimpleWave(enemy, Spawner, amount, 1 / frequency, Mathf.RoundToInt(GetEarnedFromKills (index)), Mathf.RoundToInt(GetCompletionReward(index)));
        }
    }
}
