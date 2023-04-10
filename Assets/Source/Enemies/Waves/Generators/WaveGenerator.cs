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
        private System.Random _random;
        private GeneratorEnemyData[] _enemyData;

        public WaveGenerator(GeneratorEnemyData[] enemyData)
        {
            _enemyData = enemyData;
        }

        private GeneratorEnemyData GetRandomEnemyGeneratorData (int wave)
        {
            var applicable = _enemyData.Where(x => ShouldSpawn(x, wave)).ToArray();
            float[] weights = applicable.Select(x => x.GetWeight(wave)).ToArray();
            float maxWeight = weights.Sum();
            float random = (float)_random.NextDouble() * maxWeight;

            GeneratorEnemyData selection = null;
            float cumWeight = 0;
            foreach (var option in applicable)
            {
                float w = option.GetWeight(wave);
                if (cumWeight <= random)
                {
                    selection = option;
                }
                cumWeight += w;
            }

            if (selection == null)
            {
                return null;
            }
            return selection;
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

        public bool CanGenerate(int wave) => _enemyData.Any(x => ShouldSpawn(x, wave));

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
