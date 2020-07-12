using Lomztein.BFA2.Content.Objects;
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

        private IContentCachedPrefab[] _enemies;
        private System.Random _random;

        private GameObject _spawner;

        private readonly int _seed;
        private readonly float _credits;
        private readonly float _frequency;

        private readonly float _maxSpawnFrequency;
        private readonly float _minSpawnFrequency;


        public WaveGenerator (GameObject spawner, int seed, float credits, float frequency, float maxFreq, float minFreq)
        {
            _spawner = spawner;
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
                _enemies = Content.Content.GetAll(ENEMY_CONTENT_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _enemies;
        }

        private System.Random GetRandom()
        {
            if (_random == null)
            {
                _random = new System.Random(_seed);
            }
            return _random;
        }

        private (IContentCachedPrefab enemy, int amount) GetRandomEnemy(float credits)
        {
            var enemies = GetEnemies();
            IContentCachedPrefab[] options = enemies.Where(x => ShouldSpawn(x.GetCache().GetComponent<IEnemy>())).ToArray();
            IContentCachedPrefab enemy = options[GetRandom().Next(0, options.Length)];
            return (enemy, Mathf.RoundToInt(credits / enemy.GetCache().GetComponent<IEnemy>().DifficultyValue));
        }

        private bool ShouldSpawn(IEnemy enemy)
        {
            float frequency = _frequency / enemy.DifficultyValue;
            return frequency < _maxSpawnFrequency && frequency > _minSpawnFrequency;
        }

        public IWave GenerateWave()
        {
            (IContentCachedPrefab enemy, int amount) = GetRandomEnemy(_credits);
            float frequency = _frequency / enemy.GetCache().GetComponent<IEnemy>().DifficultyValue;

            IWave newWave = new Wave(enemy, _spawner, amount, 1 / frequency);
            return newWave;
        }
    }
}
