using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
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
        private const string ENEMY_CONTENT_PATH = "*/Enemies";
        private const string GENERATOR_ENEMY_DATA_PATH = "*/WaveCollections/GeneratorEnemyData";

        private IContentCachedPrefab[] _enemies;
        private System.Random _random;

        private ContentPrefabReference _spawner;
        private GeneratorEnemyData[] _enemyData;

        private readonly int _seed;
        private readonly float _credits;
        private readonly float _frequency;
        private readonly int _wave;

        private readonly float _maxSpawnFrequency;
        private readonly float _minSpawnFrequency;


        public WaveGenerator (ContentPrefabReference spawner, int wave, int seed, float credits, float frequency, float maxFreq, float minFreq)
        {
            _spawner = spawner;
            _wave = wave;
            _seed = seed;
            _credits = credits;
            _frequency = frequency;

            _maxSpawnFrequency = maxFreq;
            _minSpawnFrequency = minFreq;
        }

        private IContentCachedPrefab[] GetEnemies()
        {
            if (_enemies == null)
            {
                _enemies = ContentSystem.Content.GetAll(ENEMY_CONTENT_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _enemies;
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

        private GeneratorEnemyData GetGeneratorEnemyData (IEnemy enemy)
            => GetGeneratorEnemyDataCache().FirstOrDefault(x => x.EnemyIdentifier == enemy.Identifier);

        private (IContentCachedPrefab enemy, int amount) GetRandomEnemy(float credits)
        {
            var enemies = GetEnemies();
            IContentCachedPrefab[] options = enemies.Where(x => ShouldSpawn(x.GetCache().GetComponent<IEnemy>())).ToArray();
            IContentCachedPrefab enemy = options[GetRandom().Next(0, options.Length)];

            GeneratorEnemyData data = GetGeneratorEnemyData(enemy.GetCache().GetComponent<IEnemy>());

            return (enemy, Mathf.Max(Mathf.RoundToInt(credits / data.DifficultyValue), 1));
        }

        private bool ShouldSpawn(IEnemy enemy)
        {
            GeneratorEnemyData data = GetGeneratorEnemyData(enemy);
            if (data == null)
                return false;

            float frequency = _frequency / data.DifficultyValue;

            return _wave >= data.EarliestWave && frequency <= _maxSpawnFrequency && frequency >= _minSpawnFrequency;
        }

        public IWave GenerateWave()
        {
            (IContentCachedPrefab enemy, int amount) = GetRandomEnemy(_credits);
            GeneratorEnemyData data = GetGeneratorEnemyData(enemy.GetCache().GetComponent<IEnemy>());

            float frequency = _frequency / data.DifficultyValue;

            IWave newWave = new Wave(enemy, _spawner, amount, 1 / frequency);
            return newWave;
        }
    }
}
