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

        private GeneratorEnemyData[] GetGeneratorEnemyDataCache ()
        {
            if (_enemyData == null)
            {
                _enemyData = Content.GetAll<GeneratorEnemyData>(GENERATOR_ENEMY_DATA_PATH);
            }
            return _enemyData;
        }

        private GeneratorEnemyData GetRandomEnemyGeneratorData (int wave)
        {
            var cache = GetGeneratorEnemyDataCache();
            var applicable = cache.Where(x => ShouldSpawn(x, wave)).ToArray();
            if (applicable.Length == 0)
            {
                return null;
            }
            int index = _random.Next(0, applicable.Length);
            return applicable[index];
        }

        private (GeneratorEnemyData data, int amount) GetRandomEnemy(float credits, int wave)
        {
            GeneratorEnemyData data = GetRandomEnemyGeneratorData(wave);
            if (data != null)
            {
                return (data, Mathf.Max(Mathf.RoundToInt(credits / data.DifficultyValue), 1));
            }
            else
            {
                return (null, 0);
            }
        }

        private bool ShouldSpawn(GeneratorEnemyData enemy, float wave)
        {
            bool shouldSpawn = wave >= enemy.EarliestWave && wave < enemy.LastWave;
            return shouldSpawn;
        }

        public SpawnInterval Generate(float startTime, int wave, int seed, float credits, float baseFrequency)
        {
            _random = new System.Random(seed);
            (GeneratorEnemyData data, int amount) = GetRandomEnemy(credits, wave);
            if (data != null)
            {
                float frequency = baseFrequency / data.DifficultyValue;
                float time = amount / frequency;
                return new SpawnInterval(startTime, time, data.EnemyIdentifier, amount);
            }
            else return null;
        }

        public bool CanGenerate(int wave) => GetGeneratorEnemyDataCache().Any(x => ShouldSpawn(x, wave));
    }
}
