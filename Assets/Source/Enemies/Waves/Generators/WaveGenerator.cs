using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Generators
{
    [Serializable]
    public class WaveGenerator : IWaveGenerator
    {
        private const string GENERATOR_ENEMY_DATA_PATH = "*/WaveCollections/GeneratorEnemyData";

        private System.Random _random;
        private GeneratorEnemyData[] _enemyData;

        private readonly float _startTime;
        private readonly int _seed;
        private readonly float _credits;
        private readonly float _frequency;
        private readonly int _wave;

        private readonly float _maxSpawnFrequency;
        private readonly float _minSpawnFrequency;


        public WaveGenerator (float startTime, int wave, int seed, float credits, float frequency, float maxFreq, float minFreq)
        {
            _startTime = startTime;
            _wave = wave;
            _seed = seed;
            _credits = credits;
            _frequency = frequency;

            _maxSpawnFrequency = maxFreq;
            _minSpawnFrequency = minFreq;
        }

        private GeneratorEnemyData[] GetGeneratorEnemyDataCache ()
        {
            if (_enemyData == null)
            {
                _enemyData = Content.GetAll<GeneratorEnemyData>(GENERATOR_ENEMY_DATA_PATH);
            }
            return _enemyData;
        }

        private System.Random GetRandom()
        {
            if (_random == null)
            {
                _random = new System.Random(_seed);
            }
            return _random;
        }

        private GeneratorEnemyData GetRandomEnemyGeneratorData ()
        {
            var cache = GetGeneratorEnemyDataCache();
            var applicable = cache.Where(x => ShouldSpawn(x)).ToArray();
            int index = GetRandom().Next(0, applicable.Length);
            return applicable[index];
        }

        private (GeneratorEnemyData data, int amount) GetRandomEnemy(float credits)
        {
            GeneratorEnemyData data = GetRandomEnemyGeneratorData();
            return (data, Mathf.Max(Mathf.RoundToInt(credits / data.DifficultyValue), 1));
        }

        private bool ShouldSpawn(GeneratorEnemyData enemy)
        {
            float frequency = _frequency / enemy.DifficultyValue;
            return _wave >= enemy.EarliestWave && frequency <= _maxSpawnFrequency && frequency >= _minSpawnFrequency;
        }

        public SpawnInterval Generate()
        {
            (GeneratorEnemyData data, int amount) = GetRandomEnemy(_credits);
            float frequency = _frequency / data.DifficultyValue;
            float time = amount / frequency;
            return new SpawnInterval(_startTime, time, data.EnemyIdentifier, amount);
        }
    }
}
