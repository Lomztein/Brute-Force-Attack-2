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
    public class RoundController : MonoBehaviour
    {
        public static RoundController Instance;
        public enum RoundState { Ready, Preparing, InProgress }

        public int NextIndex = 1;
        public WaveTimeline NextWave => GetWave(NextIndex);
        private List<WaveHandler> _activeWaves = new List<WaveHandler>();
        public WaveHandler[] ActiveWaves => _activeWaves.ToArray();

        public RoundState State { get; private set; }

        public Resource CreditsResource;
        public Resource ResearchResource;
        public GameObject WaveHandlerPrefab;
        public EnemyPather Pather;

        public WaveCollection WaveCollection;

        public event Action<int> OnWavePreparing;
        public event Action<int, WaveHandler> OnWaveStarted;
        public event Action<int, WaveHandler> OnWaveEnemiesSpawned;
        public event Action<int, WaveHandler> OnWaveFinished;
        public event Action<int> OnWavesExhausted;
        public event Action<int> OnNextWaveChanged;

        public event Action<IEnemy> OnEnemySpawned;
        public event Action<IEnemy> OnEnemyAdded;
        public event Action<IEnemy> OnEnemyKilled;
        public event Action<IEnemy> OnEnemyFinished;

        public event Action<RoundState> OnStateChanged;
        
        public event Action<WaveTimeline> OnNewWave; 
        private Dictionary<int, bool> _waveKnown = new Dictionary<int, bool>(); // Bit of a weird solution but hey it works.

        private void Awake()
        {
            Instance = this;
        }

        public void SetWaveCollection (WaveCollection collection)
        {
            WaveCollection = collection;
        }

        public void BeginNextWave()
        {
            StartCoroutine(RunNextWave());
        }

        private IEnumerator RunNextWave()
        {
            if (State == RoundState.Ready)
            {
                yield return PrepareWave();
            }

            if (Pather.AnyPathsAvailable())
            {
                int wave = NextIndex;
                NextIndex++;
                if (!StartWave(wave))
                {
                    OnWavesExhausted?.Invoke(NextIndex);
                    NextIndex--;
                }
            }
        }

        private bool IsWaveKnown(int wave)
        {
            if (_waveKnown.TryGetValue(wave, out bool value))
            {
                return value;
            }
            return false;
        }

        private void MarkWaveAsKnown (int wave, bool value)
        {
            if (_waveKnown.ContainsKey(wave))
            {
                _waveKnown[wave] = value;
            }
            else
            {
                _waveKnown.Add(wave, value);
            }
        }

        private IEnumerator PrepareWave()
        {
            OnWavePreparing?.Invoke(NextIndex);
            ChangeState (RoundState.Preparing);
            Debug.Log("Preparing to begin waves.");
            yield return Pather.ComputePaths();
        }

        public WaveTimeline GetWave(int index)
        {
            WaveTimeline timeline = WaveCollection.GetWave(index);
            if (!IsWaveKnown(index))
            {
                OnNewWave?.Invoke(timeline);
                MarkWaveAsKnown(index, true);
            }
            return timeline;
        }

        private bool StartWave(int wave)
        {
            ChangeState(RoundState.InProgress);
            WaveTimeline timeline = GetWave(wave);

            Debug.Log("Starting next wave!");

            if (timeline != null)
            {
                WaveHandler handler = Instantiate(WaveHandlerPrefab).GetComponent<WaveHandler>();
                handler.Assign(wave, timeline);
                AddWave(handler);

                handler.BeginWave();
                OnWaveStarted?.Invoke(wave, handler);
            }

            return timeline != null;
        }

        private void WaveSpawnsFinished(WaveHandler handler)
        {
            Debug.Log("Wave spawns finished");
            OnWaveEnemiesSpawned?.Invoke(handler.Wave, handler);
        }

        private void EnemySpawned(WaveHandler handler, IEnemy obj)
        {
            EnemySpawnPoint spawnpoint = Pather.GetRandomSpawnPoint();
            obj.Init(spawnpoint.transform.position, spawnpoint.GetPath(), handler);
            OnEnemySpawned?.Invoke(obj);
        }

        private void EnemyAdded(WaveHandler handler, IEnemy obj) => OnEnemyAdded?.Invoke(obj);
        private void EnemyKilled(WaveHandler handler, IEnemy obj) => OnEnemyKilled?.Invoke(obj);
        private void EnemyFinished(WaveHandler handler, IEnemy obj) => OnEnemyFinished?.Invoke(obj);

        private void ChangeState (RoundState newState)
        {
            State = newState;
            OnStateChanged?.Invoke(newState);
        }

        private void WaveCompleted(WaveHandler handler)
        {
            RemoveWave(handler);

            Player.Player.Resources.ChangeResource(ResearchResource, 1);
            OnWaveFinished?.Invoke(handler.Wave, handler);

            if (_activeWaves.Count == 0)
            {
                ChangeState(RoundState.Ready);
            }
        }

        private void AddWave (WaveHandler handler)
        {
            _activeWaves.Add(handler);

            handler.OnEnemySpawned += EnemySpawned;
            handler.OnEnemyAdded += EnemyAdded;
            handler.OnEnemyKilled += EnemyKilled;
            handler.OnEnemyFinished += EnemyFinished;
            handler.OnAllSpawnersFinished += WaveSpawnsFinished;
            handler.OnAllEnemiesDone += WaveCompleted;

            Debug.Log("Added wave " + handler.Wave);
        }

        private void RemoveWave(WaveHandler handler)
        {
            handler.OnEnemySpawned -= EnemySpawned;
            handler.OnEnemyAdded -= EnemyAdded;
            handler.OnEnemyKilled -= EnemyKilled;
            handler.OnEnemyFinished -= EnemyFinished;
            handler.OnAllSpawnersFinished -= WaveSpawnsFinished;
            handler.OnAllEnemiesDone -= WaveCompleted;

            _activeWaves.Remove(handler);
            Debug.Log("Removed wave " + handler.Wave);

            Destroy(handler.gameObject);
        }
    }
}
