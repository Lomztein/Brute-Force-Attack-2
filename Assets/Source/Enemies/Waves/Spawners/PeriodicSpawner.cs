using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
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
        public event Action OnFinished;

        public void Spawn(int amount, float delay, IContentPrefab prefab)
        {
            StartCoroutine(InternalSpawn(amount, delay, prefab));
        }

        private IEnumerator InternalSpawn(int amount, float delay, IContentPrefab prefab)
        {
            for (int i = 0; i < amount; i++)
            {
                OnSpawn?.Invoke(prefab.Instantiate());
                yield return new WaitForSeconds(delay);
            }

            OnFinished?.Invoke();
            Destroy(gameObject);
        }
    }
}
