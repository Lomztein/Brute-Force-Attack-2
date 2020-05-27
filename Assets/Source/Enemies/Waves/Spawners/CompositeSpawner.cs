using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public class CompositeSpawner : MonoBehaviour, ISpawner
    {
        public event Action<GameObject> OnSpawn;
        public float FrequencyPerSpawner = 50;
        public GameObject NestedSpawner;

        public void Spawn(int amount, float delay, IContentPrefab prefab)
        {
            int spawners = Mathf.CeilToInt((1f / delay) / FrequencyPerSpawner);
            float d = delay * spawners;
            int a = amount / spawners;

            for (int i = 0; i < spawners; i++)
            {
                InstantiateSpawner(a, d, prefab);
            }
        }

        private ISpawner InstantiateSpawner (int amount, float delay, IContentPrefab prefab)
        {
            GameObject newSpawner = Instantiate(NestedSpawner, transform);
            ISpawner spawner = newSpawner.GetComponent<ISpawner>();
            spawner.OnSpawn += OnSpawnerSpawn;
            spawner.Spawn(amount, delay, prefab);
            return spawner;
        }

        private void OnSpawnerSpawn(GameObject obj)
        {
            OnSpawn?.Invoke(obj);
        }
    }
}
