using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public interface ISpawner
    {
        event Action<GameObject> OnSpawn;
        event Action OnFinished;

        void Spawn(int amount, float delay, IContentPrefab prefab);
    }
}