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
        public ContentPrefabReference SpawnerPrefab;

        public int SpawnAmount => Mathf.RoundToInt(_baseAmount * _amountScale);
        public float SpawnDelay => 1f / (1f / _baseDelay * _frequencyScale);

        public int Alive;

        private readonly float _baseAmount;
        private readonly float _baseDelay;

        private float _amountScale = 1f;
        private float _frequencyScale = 1f;

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action OnFinished;
        public event Action OnAllSpawned;

        private GameObject _spawnerObj;

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
        
        public Wave (IContentCachedPrefab prefab, ContentPrefabReference spawner, int amount, float delay)
        {
            Prefab = prefab;
            SpawnerPrefab = spawner;
            _baseAmount = amount;
            _baseDelay = delay;
        }

        private void Spawn()
        {
            _spawnerObj = SpawnerPrefab.Instantiate();
            ISpawner spawner = _spawnerObj.GetComponent<ISpawner>();

            spawner.OnSpawn += Spawner_OnSpawn;
            spawner.OnFinished += () => OnAllSpawned?.Invoke();

            spawner.Spawn(SpawnAmount, SpawnDelay, Prefab);
        }

        private void Spawner_OnSpawn(GameObject obj)
        {
            IEnemy enemy = obj.GetComponent<IEnemy>();
            OnEnemySpawn?.Invoke(enemy);

            enemy.OnKilled += OnEnemyKilled;
            enemy.OnFinished += OnEnemyFinished;

            enemy.OnKilled += OnEnemyDeath;
            enemy.OnFinished += OnEnemyDeath;
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
