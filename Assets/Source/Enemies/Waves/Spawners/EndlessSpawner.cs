using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public class EndlessSpawner : MonoBehaviour, ISpawner
    {
        public IContentPrefab SpawnerPrefab;

        public float StepUpDelay;
        public float StepUpSpawnFrequencyMult;

        public event Action<GameObject> OnSpawn;

        private int _amount;
        private float _delay;
        private IContentPrefab _prefab;

        public void Spawn(int amount, float delay, IContentPrefab prefab)
        {
            _amount = amount;
            _delay = delay;
            _prefab = prefab;

            SpawnSpawner(amount, delay, prefab);
            InvokeRepeating("StepUp", StepUpDelay, StepUpDelay);
        }

        private void StepUp ()
        {
            float delay = _delay / StepUpSpawnFrequencyMult;
            SpawnSpawner(_amount, delay, _prefab);
        }

        private void SpawnSpawner (int amount, float delay, IContentPrefab prefab)
        {
            GameObject obj = SpawnerPrefab.Instantiate();
            ISpawner spawner = obj.GetComponent<ISpawner>();
            spawner.OnSpawn += OnNestedSpawn;
            spawner.Spawn(amount, delay, prefab);
        }

        private void OnNestedSpawn(GameObject obj)
        {
            OnSpawn?.Invoke(obj);
        }
    }
}