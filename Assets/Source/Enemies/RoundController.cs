using Lomztein.BFA2.Enemies.Waves;
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

        public int CurrentWave;
        public RoundState State;

        [SerializeField] private SimpleWaveGeneratorCollection _internalWaveCollection = new SimpleWaveGeneratorCollection();
        private IWaveCollection WaveCollection => _internalWaveCollection;
        private IResourceContainer _resourceContainer;
        private IHealthContainer _healthContainer;

        private EnemySpawnPoint[] _spawnPoints;
        private EnemyPoint[] _endPoints;

        private void Awake()
        {
            _resourceContainer = GetComponent<IResourceContainer>();
            _healthContainer = GetComponent<IHealthContainer>();
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
                CurrentWave++;
                StartWave(CurrentWave);
            }
            else
            {
                Debug.LogWarning("No paths are available for the wave to start.");
            }
        }

        private IEnumerator PrepareWave()
        {
            State = RoundState.Preparing;
            foreach (EnemySpawnPoint point in _spawnPoints)
            {
                point.ComputePath(_endPoints);
                yield return new WaitForFixedUpdate();
            }
        }

        private void StartWave(int wave)
        {
            State = RoundState.InProgress;
            IWave next = WaveCollection.GetWave(wave);
            next.OnFinished += OnWaveFinished;
            next.OnEnemySpawn += OnSpawn;
            next.Start(_resourceContainer, _healthContainer);
        }

        private bool AnyPathsAvailable() => _spawnPoints.Any(x => !x.PathBlocked);

        private void OnSpawn(IEnemy obj)
        {
            obj.Init(GetSpawnPoint());
        }

        private EnemySpawnPoint GetSpawnPoint()
        {
            EnemySpawnPoint[] available = _spawnPoints.Where(x => !x.PathBlocked).ToArray();
            return available[Random.Range(0, available.Length)];
        }

        private void OnWaveFinished()
        {
            EndWave(CurrentWave);
        }

        private void EndWave(int wave)
        {
            IWave ended = WaveCollection.GetWave(wave);
            ended.OnFinished -= OnWaveFinished;
            ended.OnEnemySpawn -= OnSpawn;
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
