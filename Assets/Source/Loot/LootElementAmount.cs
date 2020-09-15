using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Loot
{
    public class LootElementAmount
    {
        public int Amount;
        public IContentCachedPrefab Prefab;

        public LootElementAmount(int amount, IContentCachedPrefab prefab)
        {
            Amount = amount;
            Prefab = prefab;
        }

        public static LootElementAmount Empty()
            => new LootElementAmount(0, null);
    }
}
