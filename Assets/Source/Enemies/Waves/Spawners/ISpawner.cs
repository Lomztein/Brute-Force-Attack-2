using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Content.References;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves.Spawners
{
    public interface ISpawner
    {
        event Action<GameObject> OnSpawn;

        void Spawn(int amount, float delay, IContentPrefab prefab);
    }
}