using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Loot.Rules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class Roll
    {
        public IContentCachedPrefab Prefab;
        public int Amount;

        public Roll (IContentCachedPrefab basePrefab, int baseAmount)
        {
            Prefab = basePrefab;
            Amount = baseAmount;
        }
    }
}
