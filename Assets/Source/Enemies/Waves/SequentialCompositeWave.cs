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

        public int SpawnAmount => Waves.Sum(x => x.SpawnAmount);

        public readonly IWave[] Waves;

        public void Start()
        {
            foreach (IWave wave in Waves)
            {
                wave.OnEnemySpawn += (enemy) => OnEnemySpawn?.Invoke(enemy);
                wave.OnEnemyKill += (enemy) => OnEnemyKill?.Invoke(enemy);
                wave.OnEnemyFinish += (enemy) => OnEnemyFinish?.Invoke(enemy);
                wave.OnAllSpawned += () => Wave_OnAllSpawned(wave);
            }

            Waves.Last().OnFinished += SequentialCompositeWave_OnFinished;
            Waves.Last().OnAllSpawned += SequentialCompositeWave_OnAllSpawned;

            Waves.First().Start();
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
            int index = Array.IndexOf(Waves, wave);
            if (index != Waves.Length - 1)
            {
                Waves[index + 1].Start();
            }
        }

        public SequentialCompositeWave (IWave[] waves)
        {
            Waves = waves;
        }
    }
}
