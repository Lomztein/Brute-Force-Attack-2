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

        public event Action<int, IWave> OnWaveStarted;
        public event Action<int, IWave> OnWaveCleared;

        public override void OnSceneUnloaded()
        {
            if (RoundController.Instance)
            {
                RoundController.Instance.OnEnemySpawn -= OnEnemySpawn;
                RoundController.Instance.OnEnemyFinish -= OnEnemyFinish;
                RoundController.Instance.OnEnemyKill -= OnEnemyKill;

                RoundController.Instance.OnWaveStarted -= OnWaveStarted;
                RoundController.Instance.OnWaveFinished -= OnWaveCleared;
            }
        }

        public override void OnSceneLoaded()
        {
            RoundController.Instance.OnEnemySpawn += OnEnemySpawn;
            RoundController.Instance.OnEnemyFinish += OnEnemyFinish;
            RoundController.Instance.OnEnemyKill += OnEnemyKill;

            RoundController.Instance.OnWaveStarted += OnWaveStarted;
            RoundController.Instance.OnWaveFinished += OnWaveCleared;
        }
    }
}
