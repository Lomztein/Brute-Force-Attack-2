using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lomztein.BFA2.Enemies.RoundController;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class EnemySubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<int> OnWavePreparing;
        public event Action<int, WaveHandler> OnWaveStarted;
        public event Action<int, WaveHandler> OnWaveEnemiesSpawned;
        public event Action<int, WaveHandler> OnWaveFinished;
        public event Action<int> OnWavesExhausted;
        public event Action<int> OnNextWaveChanged;

        public event Action<Enemy> OnEnemySpawned;
        public event Action<Enemy> OnEnemyAdded;
        public event Action<Enemy> OnEnemyKilled;
        public event Action<Enemy> OnEnemyFinished;

        public event Action<RoundState> OnStateChanged;
        public event Action<WaveTimeline> OnNewWave;

        public override void OnSceneLoaded()
        {
            Instance.OnWavePreparing += OnWavePreparing;
            Instance.OnWaveStarted += OnWaveStarted;
            Instance.OnWaveEnemiesSpawned += OnWaveEnemiesSpawned;
            Instance.OnWaveFinished += OnWaveFinished;
            Instance.OnWavesExhausted += OnWavesExhausted;
            Instance.OnNextWaveChanged += OnNextWaveChanged;

            Instance.OnEnemySpawned += OnEnemySpawned;
            Instance.OnEnemyAdded += OnEnemyAdded;
            Instance.OnEnemyKilled += OnEnemyKilled;
            Instance.OnEnemyFinished += OnEnemyFinished;

            Instance.OnStateChanged += OnStateChanged;
            Instance.OnNewWave += OnNewWave;
        }

        public override void OnSceneUnloaded()
        {
            if (Instance)
            {
                Instance.OnWavePreparing -= OnWavePreparing;
                Instance.OnWaveStarted -= OnWaveStarted;
                Instance.OnWaveEnemiesSpawned -= OnWaveEnemiesSpawned;
                Instance.OnWaveFinished -= OnWaveFinished;
                Instance.OnWavesExhausted -= OnWavesExhausted;
                Instance.OnNextWaveChanged -= OnNextWaveChanged;

                Instance.OnEnemySpawned -= OnEnemySpawned;
                Instance.OnEnemyAdded -= OnEnemyAdded;
                Instance.OnEnemyKilled -= OnEnemyKilled;
                Instance.OnEnemyFinished -= OnEnemyFinished;

                Instance.OnStateChanged -= OnStateChanged;
                Instance.OnNewWave -= OnNewWave;
            }
        }
    }
}
