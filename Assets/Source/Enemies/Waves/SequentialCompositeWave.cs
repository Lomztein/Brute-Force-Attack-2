using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Player.Health;
using Lomztein.BFA2.Purchasing.Resources;

namespace Lomztein.BFA2.Enemies.Waves
{
    public class SequentialCompositeWave : IWave
    {
        public event Action<IEnemy> OnEnemySpawn;
        public event Action<IEnemy> OnEnemyKill;
        public event Action<IEnemy> OnEnemyFinish;

        public event Action OnFinished;
        public event Action OnAllSpawned;

        public int SpawnAmount => _waves.Sum(x => x.SpawnAmount);

        private readonly IWave[] _waves;

        public void Start()
        {
            foreach (IWave wave in _waves)
            {
                wave.OnEnemySpawn += (enemy) => OnEnemySpawn?.Invoke(enemy);
                wave.OnEnemyKill += (enemy) => OnEnemyKill?.Invoke(enemy);
                wave.OnEnemyFinish += (enemy) => OnEnemyFinish?.Invoke(enemy);
                wave.OnAllSpawned += () => Wave_OnAllSpawned(wave);
            }

            _waves.Last().OnFinished += SequentialCompositeWave_OnFinished;
            _waves.Last().OnAllSpawned += SequentialCompositeWave_OnAllSpawned;

            _waves.First().Start();
        }

        private void SequentialCompositeWave_OnAllSpawned()
        {
            OnAllSpawned?.Invoke();
        }

        private void SequentialCompositeWave_OnFinished()
        {
            OnFinished?.Invoke();
        }

        // Stuff gets wierd when you work with anonymous methods.
        private void Wave_OnAllSpawned(IWave wave)
        {
            int index = Array.IndexOf(_waves, wave);
            if (index != _waves.Length - 1)
            {
                _waves[index + 1].Start();
            }
        }

        public SequentialCompositeWave (IWave[] waves)
        {
            _waves = waves;
        }
    }
}
