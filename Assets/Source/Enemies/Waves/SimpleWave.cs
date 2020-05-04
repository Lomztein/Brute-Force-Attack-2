using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class SimpleWave : IWave
    {
        public float SpawnDelay;
        public GameObject Prefab;

        public int SpawnAmount;
        public int Alive;

        public event Action<IEnemy> OnSpawn;
        public event Action OnFinished;
        private IRoundController _manager;

        public void Start(IRoundController manager)
        {
            _manager = manager;
            Alive = SpawnAmount;
            _manager.InvokeDelayed(() => Spawn(), SpawnDelay);
        }

        private void Spawn()
        {
            IEnemy enemy = UnityEngine.Object.Instantiate(Prefab).GetComponent<IEnemy>();
            OnSpawn?.Invoke(enemy);
            enemy.SetOnDeathCallback(() => OnEnemyKill ());
            SpawnAmount--;

            if (SpawnAmount > 0)
            {
                _manager.InvokeDelayed(() => Spawn(), SpawnDelay);
            }
        }

        private void OnEnemyKill()
        {
            Alive--;
            if (Alive == 0)
            {
                OnFinished?.Invoke();
            }
        }
    }
}
