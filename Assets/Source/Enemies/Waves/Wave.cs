using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies.Waves.Spawners;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class Wave : IWave
    {
        public IContentCachedPrefab Prefab;
        public GameObject SpawnerPrefab;

        public int SpawnAmount => Mathf.RoundToInt(_baseAmount * _amountScale);
        public float SpawnDelay => 1f / (1f / _baseDelay * _frequencyScale);

        public int Alive;

        private float _baseAmount;
        private float _baseDelay;

        private float _amountScale = 1f;
        private float _frequencyScale = 1f;

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action OnFinished;
        public event Action OnAllSpawned;

        public void Start()
        {
            Alive = SpawnAmount;
            Spawn();
        }

        public void SetScale (float amount, float frequency)
        {
            _amountScale = amount;
            _frequencyScale = frequency;
        }
        
        public Wave (IContentCachedPrefab prefab, GameObject spawner, int amount, float delay)
        {
            Prefab = prefab;
            SpawnerPrefab = spawner;
            _baseAmount = amount;
            _baseDelay = delay;
        }

        private void Spawn()
        {
            ISpawner spawner = UnityEngine.Object.Instantiate(SpawnerPrefab).GetComponent<ISpawner>();

            spawner.OnSpawn += Spawner_OnSpawn;
            spawner.OnFinished += () => OnAllSpawned?.Invoke();

            spawner.Spawn(SpawnAmount, SpawnDelay, Prefab);
        }

        private void Spawner_OnSpawn(GameObject obj)
        {
            IEnemy enemy = obj.GetComponent<IEnemy>();
            OnEnemySpawn?.Invoke(enemy);

            enemy.OnKilled += OnEnemyDeath;
            enemy.OnFinished += OnEnemyDeath;

            enemy.OnKilled += OnEnemyKilled;
            enemy.OnFinished += OnEnemyFinished;
        }

        private void OnEnemyFinished(IEnemy obj)
        {
            OnEnemyFinish?.Invoke(obj);
        }

        private void OnEnemyKilled(IEnemy obj)
        {
            OnEnemyKill?.Invoke(obj);
        }

        private void OnEnemyDeath(IEnemy enemy)
        {
            Alive--;
            if (Alive == 0)
            {
                OnFinished?.Invoke();
            }
        }
    }
}
