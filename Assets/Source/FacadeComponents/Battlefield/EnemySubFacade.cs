using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.FacadeComponents.Battlefield
{
    public class EnemySubFacade : SceneFacadeSubComponent<BattlefieldFacade>
    {
        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action<IEnemy> OnEnemyKill;

        public event Action<int, WaveHandler> OnWaveStarted;
        public event Action<int, WaveHandler> OnWaveEnemiesSpawned;
        public event Action<int, WaveHandler> OnWaveCleared;

        public override void OnSceneUnloaded()
        {
            if (RoundController.Instance)
            {
                RoundController.Instance.OnEnemyAdded -= OnEnemySpawn;
                RoundController.Instance.OnEnemyFinished -= OnEnemyFinish;
                RoundController.Instance.OnEnemyKilled -= OnEnemyKill;

                RoundController.Instance.OnWaveStarted -= OnWaveStarted;
                RoundController.Instance.OnWaveEnemiesSpawned -= OnWaveEnemiesSpawned;
                RoundController.Instance.OnWaveFinished -= OnWaveCleared;
            }
        }

        public override void OnSceneLoaded()
        {
            RoundController.Instance.OnEnemyAdded += OnEnemySpawn;
            RoundController.Instance.OnEnemyFinished += OnEnemyFinish;
            RoundController.Instance.OnEnemyKilled += OnEnemyKill;

            RoundController.Instance.OnWaveStarted += OnWaveStarted;
            RoundController.Instance.OnWaveEnemiesSpawned += OnWaveEnemiesSpawned;
            RoundController.Instance.OnWaveFinished += OnWaveCleared;
        }
    }
}
