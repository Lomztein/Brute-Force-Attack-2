﻿using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Utilities;
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
                yield return UnityUtils.WaitForFixedSeconds(delay);
            }

            OnFinished?.Invoke();
            Destroy(gameObject);
        }
    }
}
