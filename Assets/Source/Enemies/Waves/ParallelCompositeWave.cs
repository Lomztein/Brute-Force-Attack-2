using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class ParallelCompositeWave : IWave
    {
        public int SpawnAmount => _waves.Sum(x => x.SpawnAmount);

        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;
        public event Action OnFinished;
        public event Action OnAllSpawned;

        private readonly IWave[] _waves;

        private int _finished;
        private int _allSpawned;


        public void Start()
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                IWave wave = _waves[i];

                wave.OnEnemySpawn += (enemy) => OnEnemySpawn?.Invoke(enemy);
                wave.OnEnemyKill += (enemy) => OnEnemyKill?.Invoke(enemy);
                wave.OnEnemyFinish += (enemy) => OnEnemyFinish?.Invoke(enemy);

                wave.OnFinished += OnWaveFinished;
                wave.OnAllSpawned += OnWaveAllSpawned;

                wave.Start();
            }
        }

        private void OnWaveAllSpawned()
        {
            _allSpawned++;
            if (_allSpawned == _waves.Length)
            {
                OnAllSpawned?.Invoke();
            }
        }

        private void OnWaveFinished()
        {
            _finished++;
            if (_finished == _waves.Length)
            {
                OnFinished?.Invoke();
            }
        }

        public ParallelCompositeWave (IWave[] waves)
        {
            _waves = waves;
        }
    }
}
