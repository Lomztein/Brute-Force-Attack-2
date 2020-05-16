using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Content.References.GameObjects;
using Lomztein.BFA2.Serialization.Models.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class SimpleWaveGeneratorCollection : IWaveCollection
    {
        // TODO: Create a common access point for these paths to allow for easier changes.
        private const string ENEMY_CONTENT_PATH = "*/Enemies";
        private const string SPAWNER_PATH = "Core/EnemySpawners/PeriodicSpawner.json";

        private CachedGameObject[] _enemies;
        private System.Random _random;

        public GameObject Spawner;

        public int Seed;
        public float StartingCredits;
        public float Coeffecient;

        public float MaxSpawnFrequency = 50;
        public float MinSpawnFrequency = 2;

        public AnimationCurve MaxSpawnFrequencyCurve;
        public AnimationCurve MinSpawnFrequencyCurve;

        public int MaxSpawnDelayWave;

        private CachedGameObject[] GetEnemies ()
        {
            if (_enemies == null)
            {
                var models = Content.Content.GetAll(ENEMY_CONTENT_PATH, typeof(IGameObjectModel));
                List<CachedGameObject> enemies = new List<CachedGameObject>();
                foreach (IGameObjectModel model in models)
                {
                    enemies.Add(new CachedGameObject(new ContentGameObjectModel(model)));
                }
                _enemies = enemies.ToArray();
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

        private (CachedGameObject enemy, int amount) GetRandomEnemy (float credits)
        {
            var enemies = GetEnemies();
            CachedGameObject enemy = enemies[_random.Next(0, enemies.Length)];
            IEnemy component = enemy.Get().GetComponent<IEnemy>();
            return (enemy, Mathf.RoundToInt(credits / component.DifficultyValue));
        }

        private float GetSpawnFrequency (int wave)
        {
            float tw = Mathf.Min (wave / (float)MaxSpawnDelayWave, 1f);
            float min = MinSpawnFrequencyCurve.Evaluate(tw);
            float max = MaxSpawnFrequencyCurve.Evaluate(tw);
            float t = Mathf.Lerp(min, max, (float)GetRandom().NextDouble());
            return Mathf.Lerp(MinSpawnFrequency, MaxSpawnFrequency, t);
        }

        private IContentGameObject GetSpawner ()
        {
            return new ContentGameObjectModel(SPAWNER_PATH);
        }

        private float GetAvailableCredits(int wave) => StartingCredits + Mathf.Pow(Coeffecient, wave);

        public IWave GetWave(int index)
        {
            float credits = GetAvailableCredits(index);
            float frequency = GetSpawnFrequency(index);

            (CachedGameObject enemy, int amount) = GetRandomEnemy(credits);

            return new SimpleWave(enemy, Spawner, amount, 1 / frequency);
        }
    }
}
