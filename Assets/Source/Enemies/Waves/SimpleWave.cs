using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Enemies.Waves.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class SimpleWave : IWave
    {
        public float SpawnDelay;
        public IContentPrefab Prefab;
        public GameObject SpawnerPrefab;

        public int SpawnAmount;
        public int Alive;

        public event Action<IEnemy> OnSpawn;
        public event Action OnFinished;

        public void Start()
        {
            Spawn();
            Alive = SpawnAmount;
        }
        
        public SimpleWave (IContentPrefab prefab, GameObject spawner, int amount, float delay)
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

            spawner.Spawn(SpawnAmount, SpawnDelay, Prefab);
        }

        private void Spawner_OnSpawn(GameObject obj)
        {
            IEnemy enemy = obj.GetComponent<IEnemy>();
            OnSpawn?.Invoke(enemy);

            enemy.OnDeath += (x, y) => OnEnemyDeath(x);
            enemy.OnFinished += (x, y) => OnEnemyDeath(x);
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
