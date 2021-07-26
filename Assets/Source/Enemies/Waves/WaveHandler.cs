using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Enemies.Waves.Punishers;
using Lomztein.BFA2.Enemies.Waves.Rewarders;
using Lomztein.BFA2.Enemies.Waves.Spawners;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class WaveHandler : MonoBehaviour
    {
        public const string ENEMY_PATH = "*/Enemies";
        public GameObject SpawnerPrefab;
        public WaveTimeline Timeline;
        public int Wave;

        private IContentCachedPrefab[] _enemies;

        public event Action<WaveHandler, SpawnInterval> OnSpawnerFinished;
        public event Action<WaveHandler> OnAllSpawnersFinished;
        public event Action<WaveHandler> OnAllEnemiesDone;

        public event Action<WaveHandler, IEnemy> OnEnemySpawned;
        public event Action<WaveHandler, IEnemy> OnEnemyAdded;
        public event Action<WaveHandler, IEnemy> OnEnemyKilled;
        public event Action<WaveHandler, IEnemy> OnEnemyFinished;

        private int _totalAmount;
        private int _done;

        private int _spawnerAmount;
        private int _spawnersFinished;

        public void Awake()
        {
            _enemies = LoadEnemies();
        }


        private IContentCachedPrefab[] LoadEnemies() => Content.GetAll<IContentCachedPrefab>(ENEMY_PATH);
        private IContentCachedPrefab GetPrefab(string identifier) => _enemies.FirstOrDefault(x => x.GetCache().GetComponent<IEnemy>().Identifier == identifier);

        public void Assign (int wave, WaveTimeline timeline)
        {
            Wave = wave;
            Timeline = timeline;
        }

        public void BeginWave ()
        {
            _totalAmount = Timeline.Amount;
            _spawnerAmount = Timeline.IntervalAmount;
            foreach (SpawnInterval spawn in Timeline)
            {
                StartCoroutine(HandleSpawn(spawn));
            }
        }
        
        private IEnumerator HandleSpawn (SpawnInterval spawn)
        {
            yield return new WaitForSeconds(spawn.StartTime);
            ISpawner spawner = Instantiate(SpawnerPrefab).GetComponent<ISpawner>();

            float delay = spawn.Length / spawn.Amount;

            spawner.OnFinished += () => Spawner_OnFinished(spawn);
            spawner.OnSpawn += Spawner_OnSpawn;

            spawner.Spawn(spawn.Amount, delay, GetPrefab(spawn.EnemyIdentifier));
        }

        private void Spawner_OnSpawn(GameObject obj)
        {
            IEnemy enemy = obj.GetComponent<IEnemy>();

            AddEnemy(enemy);
            OnEnemySpawned?.Invoke(this, enemy);
            enemy.OnKilled += Enemy_OnKilled;
        }

        public void AddEnemy (IEnemy enemy)
        {
            enemy.OnFinished += Enemy_OnFinished;
            OnEnemyAdded?.Invoke(this, enemy);
        }

        private void Enemy_OnFinished(IEnemy obj)
        {
            OnEnemyDone();
            Timeline.Punisher.Punish(obj);
            OnEnemyFinished?.Invoke(this, obj);
        }

        private void Enemy_OnKilled(IEnemy obj)
        {
            OnEnemyDone();
            obj.OnKilled -= Enemy_OnKilled;
            Timeline.Rewarder.OnKill(obj);
            OnEnemyKilled?.Invoke(this, obj);
        }

        private void OnEnemyDone ()
        {
            _done++;
            if (_done == _totalAmount)
            {
                OnAllEnemiesDone?.Invoke(this);
                Timeline.Rewarder.OnFinished();
            }
        }

        private void Spawner_OnFinished(SpawnInterval spawn)
        {
            _spawnersFinished++;
            if (_spawnersFinished == _spawnerAmount)
            {
                OnAllSpawnersFinished?.Invoke(this);
            }
            OnSpawnerFinished?.Invoke(this, spawn);
        }
    }
}
