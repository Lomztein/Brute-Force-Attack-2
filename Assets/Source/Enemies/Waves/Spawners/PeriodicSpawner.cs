using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public class PeriodicSpawner : MonoBehaviour, ISpawner
    {
        public event Action<GameObject> OnSpawn;

        public void Spawn(int amount, float delay, GameObject prefab)
        {
            StartCoroutine(InternalSpawn(amount, delay, prefab));
        }

        private IEnumerator InternalSpawn(int amount, float delay, GameObject prefab)
        {
            for (int i = 0; i < amount; i++)
            {
                OnSpawn?.Invoke(Instantiate(prefab));
                yield return new WaitForSeconds(delay);
            }
            Destroy(gameObject);
        }
    }
}
