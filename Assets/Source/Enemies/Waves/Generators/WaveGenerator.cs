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
        private const float MaxSpawnFrequency = 100f;

        private IContentCachedPrefab[] _enemies;
        private System.Random _random;

        private GameObject _spawner;

        private int _wave;
        private int _seed;
        private float _credits;
        private float _frequency;

        public WaveGenerator (GameObject spawner, int wave, int seed, float credits, float frequency)
        {
            _spawner = spawner;
            _wave = wave;
            _seed = seed;
            _credits = credits;
            _frequency = frequency;
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

        private (IContentCachedPrefab enemy, int amount) GetRandomEnemy(float credits, int wave)
        {
            var enemies = GetEnemies();
            IContentCachedPrefab[] options = enemies.Where(x => IsWithinDifficultyRange(x.GetCache().GetComponent<IEnemy>(), wave)).ToArray();
            IContentCachedPrefab enemy = options[GetRandom().Next(0, options.Length)];
            return (enemy, Mathf.RoundToInt(credits / enemy.GetCache().GetComponent<IEnemy>().DifficultyValue));
        }

        private bool IsWithinDifficultyRange(IEnemy enemy, int wave)
        {
            return _frequency / enemy.DifficultyValue < MaxSpawnFrequency && enemy.DifficultyValue < GetMaxDifficultyValue(wave);
        }

        private float GetMaxDifficultyValue(int wave) => wave;

        public IWave GenerateWave()
        {
            (IContentCachedPrefab enemy, int amount) = GetRandomEnemy(_credits, _wave);
            float frequency = _frequency / enemy.GetCache().GetComponent<IEnemy>().DifficultyValue;

            IWave newWave = new Wave(enemy, _spawner, amount, 1 / frequency);
            return newWave;
        }
    }
}
