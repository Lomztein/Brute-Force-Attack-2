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
        private const string GENERATOR_ENEMY_DATA_PATH = "*/WaveCollections/GeneratorEnemyData/*";

        private System.Random _random;
        private GeneratorEnemyData[] _enemyData;

        private GeneratorEnemyData[] GetGeneratorEnemyDataCache ()
        {
            if (_enemyData == null)
            {
                _enemyData = Content.GetAll<GeneratorEnemyData>(GENERATOR_ENEMY_DATA_PATH).ToArray();
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
                return (data, Math.Max((int)Math.Round(credits / data.DifficultyValue), 1));
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

        public bool CanGenerate(int wave) => GetGeneratorEnemyDataCache().Any(x => ShouldSpawn(x, wave));

        public SpawnInterval Generate(float startTime, float length, float baseFrequency, int wave, int seed)
        {
            _random = new System.Random(seed);
            float credits = baseFrequency * length;
            (GeneratorEnemyData data, int amount) = GetRandomEnemy(credits, wave);

            if (data != null)
            {
                return new SpawnInterval(startTime, (float)length, data.EnemyIdentifier, amount);
            }
            else return null;
        }
    }
}
