using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
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
        public float SpawnDelay;
        public IContentPrefab Prefab;
        public GameObject SpawnerPrefab;

        public int SpawnAmount { get; private set;  }
        public int Alive;

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action OnFinished;
        public event Action OnAllSpawned;

        public void Start()
        {
            Spawn();
            Alive = SpawnAmount;
        }
        
        public Wave (IContentPrefab prefab, GameObject spawner, int amount, float delay)
        {
            Prefab = prefab;
            SpawnerPrefab = spawner;
            SpawnAmount = amount;
            SpawnDelay = delay;
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
