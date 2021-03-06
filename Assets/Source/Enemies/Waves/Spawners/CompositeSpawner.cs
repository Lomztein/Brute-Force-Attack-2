﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public class CompositeSpawner : MonoBehaviour, ISpawner
    {
        public event Action<GameObject> OnSpawn;
        public event Action OnFinished;

        public float FrequencyPerSpawner = 50;
        public GameObject NestedSpawner;

        private int _totalSpawners;
        private int _finishedSpawners;

        public void Spawn(int amount, float delay, IContentPrefab prefab)
        {
            float f = 1f / delay;
            int spawners = Mathf.CeilToInt(f / FrequencyPerSpawner);
            float d = delay * spawners;
            int a = Mathf.RoundToInt(amount / spawners);
            int remaining = amount - (a * spawners);

            _totalSpawners = spawners;
            for (int i = 0; i < spawners; i++)
            {
                InstantiateSpawner(a, d, prefab);
            }

            // Dirty solution but of little consequence.
            if (remaining > 0)
            {
                _totalSpawners++;
                InstantiateSpawner(remaining, 1 / f / remaining, prefab);
            }
        }

        private ISpawner InstantiateSpawner (int amount, float delay, IContentPrefab prefab)
        {
            GameObject newSpawner = Instantiate(NestedSpawner);
            ISpawner spawner = newSpawner.GetComponent<ISpawner>();
            spawner.OnSpawn += OnSpawnerSpawn;
            spawner.OnFinished += OnSpawnerFinished;
            spawner.Spawn(amount, delay, prefab);
            return spawner;
        }

        private void OnSpawnerFinished()
        {
            _finishedSpawners++;
            if (_finishedSpawners == _totalSpawners)
            {
                OnFinished?.Invoke();
                Destroy(gameObject);
            }
        }

        private void OnSpawnerSpawn(GameObject obj)
        {
            OnSpawn?.Invoke(obj);
        }
    }
}
