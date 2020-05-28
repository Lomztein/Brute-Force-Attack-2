using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Enemies.Waves.Spawners;
using Lomztein.BFA2.Purchasing.Resources;
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

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action OnFinished;

        public int EarnedFromKills;
        public int CompletionReward;

        private IResourceContainer _rewardTarget;
        private float _earnings;

        public void Start(IResourceContainer rewardTarget) // TODO: Delegate rewarding to a different class, like a WaveRewarder or something.
        {
            Spawn();
            Alive = SpawnAmount;
            _rewardTarget = rewardTarget;
        }
        
        public SimpleWave (IContentPrefab prefab, GameObject spawner, int amount, float delay, int earnedFromKills, int reward)
        {
            Prefab = prefab;
            SpawnerPrefab = spawner;
            SpawnAmount = amount;
            SpawnDelay = delay;

            EarnedFromKills = earnedFromKills;
            CompletionReward = reward;
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
            Earn((float)EarnedFromKills / SpawnAmount);
        }

        private void Earn (float value)
        {
            _earnings += value;
            int floored = Mathf.FloorToInt(_earnings);
            _rewardTarget.ChangeResource(Resource.Credits, floored);
            _earnings -= floored;
        }

        private void OnEnemyDeath(IEnemy enemy)
        {
            Alive--;
            if (Alive == 0)
            {
                OnFinished?.Invoke();
                _rewardTarget.ChangeResource(Resource.Credits, CompletionReward);
            }
        }
    }
}
