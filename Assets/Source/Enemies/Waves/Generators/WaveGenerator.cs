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

        private const int BASIC_ENEMY_HEALTH = 10;
        private const int BASIC_ENEMY_SPEED = 5;

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
        {
            GeneratorEnemyData data = GetGeneratorEnemyDataCache().FirstOrDefault(x => x.EnemyIdentifier == enemy.UniqueIdentifier);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<GeneratorEnemyData>();
                data.DifficultyValue = CalculateEnemyDifficulty(enemy);
                data.EarliestWave = Mathf.RoundToInt (Mathf.Log10(data.DifficultyValue));
                data.EnemyIdentifier = enemy.UniqueIdentifier;
                Debug.LogWarning($"Enemy type {enemy.UniqueIdentifier} missing data for wave generator to use. Please create this data and place it in $ContentPack/WaveCollections/GeneratorEnemyData/");
            }
            return data;
        }

        private float CalculateEnemyDifficulty (IEnemy enemy)
        {
            if (enemy is Enemy concrete)
            {
                return (concrete.MaxHealth / BASIC_ENEMY_HEALTH) * (concrete.Speed / BASIC_ENEMY_SPEED) * ((concrete.Armor + concrete.Shields) / concrete.MaxHealth);
            }
            else
            {
                return 1; // fuck I dunno I really need to remove the IEnemy interface it's truly nothing but trouble and serves no purpose.
            }
        }

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
