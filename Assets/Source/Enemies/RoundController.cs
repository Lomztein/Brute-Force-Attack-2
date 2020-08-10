using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Enemies.Waves.Punishers;
using Lomztein.BFA2.Enemies.Waves.Rewarders;
using Lomztein.BFA2.Loot;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.World;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies
{
    public class RoundController : MonoBehaviour, IRoundController
    {
        public enum RoundState { Ready, Preparing, InProgress }

        public int CurrentWaveIndex;
        public IWave CurrentWave { get; private set; }

        public RoundState State;

        [SerializeField] private GeneratorWaveCollection _internalWaveCollection = new GeneratorWaveCollection();
        private IWaveCollection WaveCollection => _internalWaveCollection;

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

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;

        private void Awake()
        {
            _resourceContainer = GetComponent<IResourceContainer>();
            _healthContainer = GetComponent<IHealthContainer>();
            _commonLootTable = GetComponent<LootTableProvider>().GetLootTable();
        }

        private void Start()
        {
            CachePoints();
        }

        private void CachePoints ()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").Select(x => x.GetComponent<EnemySpawnPoint>()).ToArray();
            _endPoints = GameObject.FindGameObjectsWithTag("EnemyEndPoint").Select(x => x.GetComponent<EnemyPoint>()).ToArray();
        }

        private void Update()
        {
            if (State == RoundState.Ready && Input.GetButtonDown("StartWave"))
            {
                StartCoroutine (RunNextWave());
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
        }

        private bool StartWave(int wave)
        {
            State = RoundState.InProgress;
            IWave next = WaveCollection.GetWave(wave);

            if (next != null)
            {
                next.OnEnemySpawn += OnSpawn;

                IWaveRewarder rewarder = new FractionalWaveRewarder(next.SpawnAmount, GetCompletionReward(wave), GetEarnedFromKills(wave), _resourceContainer);
                IWavePunisher punshier = new FractionalWavePunisher(next.SpawnAmount, _healthContainer);

                next.OnEnemyKill += rewarder.OnKill;
                next.OnEnemyKill += EnemyKill;

                next.OnFinished += rewarder.OnFinished;
                next.OnFinished += WaveFinished;

                next.OnEnemyFinish += punshier.Punish;
                next.OnEnemyFinish += EnemyFinished;

                next.Start();
                CurrentWave = next;

                OnWaveStarted?.Invoke(wave, next);
            }

            return next != null;
        }

        private void EnemyFinished(IEnemy obj)
        {
            OnEnemyFinish?.Invoke(obj);
        }

        private void EnemyKill(IEnemy obj)
        {
            OnEnemyKill?.Invoke(obj);
            RandomizedLoot loot = _commonLootTable.GetRandomLoot((100f / CurrentWave.SpawnAmount) * CurrentWaveIndex / LootChanceGrowthDenominator, 1);
            if (!loot.Empty)
            {
                if (obj is Component comp)
                {
                    loot.InstantiateLoot(comp.transform.position, 3f);
                }
            }
        }

        private float GetEarnedFromKills(int wave) => StartingEarnedFromKills + EarnedFromKillsPerWave * (wave - 1);

        private float GetCompletionReward(int wave) => StartingCompletionReward + WaveFinishedRewardPerWave * (wave - 1);

        private bool AnyPathsAvailable() => _spawnPoints.Any(x => !x.PathBlocked);

        private void OnSpawn(IEnemy obj)
        {
            obj.Init(GetSpawnPoint());
            OnEnemySpawn?.Invoke(obj);
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
            IWave ended = WaveCollection.GetWave(wave);
            OnWaveFinished?.Invoke(wave, ended);

            State = RoundState.Ready;
            _resourceContainer.ChangeResource(Resource.Research, 1);
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
