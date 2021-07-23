using Lomztein.BFA2.Enemies.Scalers;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Enemies.Waves.Punishers;
using Lomztein.BFA2.Enemies.Waves.Rewarders;
using Lomztein.BFA2.Loot;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Player.Interrupt;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies
{
    public class RoundController : MonoBehaviour, IRoundController
    {
        public static RoundController Instance;
        public enum RoundState { Ready, Preparing, InProgress }

        public int CurrentWaveIndex;
        public IWave CurrentWave { get; private set; }

        public RoundState State;

        public Resource CreditsResource;
        public Resource ResearchResource;

        [SerializeField] private GeneratorWaveCollection _internalWaveCollection = new GeneratorWaveCollection();
        private IWaveCollection _waveCollection;

        public float EnemyAmountMultiplier;
        public float SpawnFrequencyMultiplier;

        public float StartingEarnedFromKills;
        public float EarnedFromKillsPerWave;

        public float StartingCompletionReward;
        public float WaveFinishedRewardPerWave;


        private ILootTable _commonLootTable;
        public float LootChanceGrowthDenominator;

        private IResourceContainer _resourceContainer;
        private IHealthContainer _healthContainer;

        private EnemySpawnPoint[] _spawnPoints;
        private EnemyPoint[] _endPoints;

        public event Action<int> OnWavePreparing;
        public event Action<int, IWave> OnWaveStarted;
        public event Action<int, IWave> OnWaveFinished;
        public event Action<int, string> OnWaveCancelled;
        public event Action<int> OnWavesExhausted;
        public event Action<int> OnWaveSet;

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;

        private void Awake()
        {
            Instance = this;
            _resourceContainer = GetComponent<IResourceContainer>();
            _healthContainer = GetComponent<IHealthContainer>();
            _commonLootTable = GetComponent<LootTableProvider>().GetLootTable();
            SetWaveCollection(_internalWaveCollection);
        }

        public void SetWaveCollection (IWaveCollection collection)
        {
            _waveCollection = collection;
        }

        private void Start()
        {
            _internalWaveCollection.Seed = Random.Range(int.MinValue / 2, int.MaxValue / 2);
            CachePoints();
        }

        private void CachePoints ()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Select(x => x.GetComponent<EnemySpawnPoint>()).ToArray();
            _endPoints = GameObject.FindGameObjectsWithTag("EnemyEndPoint").Select(x => x.GetComponent<EnemyPoint>()).ToArray();
        }

        public void BeginNextWave ()
        {
            if (State == RoundState.Ready)
            {
                StartCoroutine(RunNextWave());
            }
        }

        private IEnumerator RunNextWave()
        {
            yield return PrepareWave();
            if (AnyPathsAvailable())
            {
                CurrentWaveIndex++;
                if (!StartWave(CurrentWaveIndex))
                {
                    OnWavesExhausted?.Invoke(CurrentWaveIndex);
                    CancelWave("Out of waves.");
                }
            }
            else
            {
                CancelWave("No paths are available.");
            }
        }

        private IEnumerator PrepareWave()
        {
            OnWavePreparing?.Invoke(CurrentWaveIndex + 1);
            State = RoundState.Preparing;

            Debug.Log("Preparing next wave.");

            foreach (EnemySpawnPoint point in _spawnPoints)
            {
                point.ComputePath(_endPoints);
                yield return new WaitForFixedUpdate();
            }
        }

        private void CancelWave (string reason)
        {
            OnWaveCancelled?.Invoke(CurrentWaveIndex, reason);
            CurrentWaveIndex--;
            CurrentWave = null;
            State = RoundState.Ready;

            Debug.Log("Wave cancelled. Reason: " + reason);
        }

        public IWave GetWave(int index)
        {
            IWave wave = _waveCollection.GetWave(index);
            wave.SetScale(EnemyAmountMultiplier, SpawnFrequencyMultiplier);
            return wave;
        }

        private bool StartWave(int wave)
        {
            State = RoundState.InProgress;
            IWave next = GetWave(wave);

            Debug.Log("Starting next wave!");

            if (next != null)
            {
                next.OnEnemySpawn += OnSpawn;

                IWaveRewarder rewarder = new FractionalWaveRewarder(next.SpawnAmount, GetCompletionReward(wave), GetEarnedFromKills(wave), RewardCredits);
                IWavePunisher punshier = new FractionalWavePunisher(next.SpawnAmount, _healthContainer);

                next.OnEnemyKill += rewarder.OnKill;
                next.OnEnemyFinish += punshier.Punish;

                next.OnFinished += rewarder.OnFinished;
                next.OnFinished += WaveFinished;


                next.Start();
                CurrentWave = next;

                OnWaveStarted?.Invoke(wave, next);
            }

            return next != null;
        }

        private void RewardCredits (float credits)
        {
            Player.Player.Instance.Earn(CreditsResource, credits);
        }

        private void EnemyFinished(IEnemy obj)
        {
            OnEnemyFinish?.Invoke(obj);
            RemoveEnemy(obj);
        }

        private void EnemyKill(IEnemy obj)
        {
            OnEnemyKill?.Invoke(obj);
            RemoveEnemy(obj);
        }

        private float GetEarnedFromKills(int wave) => StartingEarnedFromKills + EarnedFromKillsPerWave * (wave - 1);

        private float GetCompletionReward(int wave) => StartingCompletionReward + WaveFinishedRewardPerWave * (wave - 1);

        private bool AnyPathsAvailable() => _spawnPoints.Any(x => !x.PathBlocked);

        private void OnSpawn(IEnemy obj)
        {
            EnemySpawnPoint spawnpoint = GetSpawnPoint();
            obj.Init(spawnpoint.transform.position, spawnpoint.GetPath());
            AddEnemy(obj);

            OnEnemySpawn?.Invoke(obj);
        }

        public void AddEnemy (IEnemy enemy) // Should provide a good starting point for future jobified enemy movement system.
        {
            enemy.OnKilled += EnemyKill;
            enemy.OnFinished += EnemyFinished;
        }

        public void RemoveEnemy (IEnemy enemy)
        {
            enemy.OnKilled -= EnemyKill;
            enemy.OnFinished -= EnemyFinished;
        }

        private EnemySpawnPoint GetSpawnPoint()
        {
            EnemySpawnPoint[] available = _spawnPoints.Where(x => !x.PathBlocked).ToArray();
            return available[Random.Range(0, available.Length)];
        }

        private void WaveFinished()
        {
            EndWave(CurrentWaveIndex);
        }

        private void EndWave(int wave)
        {
            if (State == RoundState.InProgress)
            {
                IWave ended = _waveCollection.GetWave(wave);
                State = RoundState.Ready;
                _resourceContainer.ChangeResource(ResearchResource, 1);

                OnWaveFinished?.Invoke(wave, ended);
                Debug.Log("Wave finished.");
            }
        }

        private void OnDrawGizmos()
        {
            if (_spawnPoints != null)
            {
                foreach (EnemySpawnPoint point in _spawnPoints)
                {
                    if (!point.PathBlocked)
                    {
                        Vector3[] path = point.GetPath();
                        for (int i = 0; i < path.Length - 1; i++)
                        {
                            Gizmos.DrawLine(path[i], path[i + 1]);
                        }
                    }
                }
            }
        }
    }
}
