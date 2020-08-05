using Lomztein.BFA2.Content.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Loot
{
    public class LootElement
    {
        public Vector2Int Amount;
        public float Chance;
        public IContentCachedPrefab Prefab;

        public LootElement(Vector2Int amount, float chance, IContentCachedPrefab prefab)
        {
            Amount = amount;
            Chance = chance;
            Prefab = prefab;
        }

        public LootElementAmount GetRandomAmount (float chanceScalar, float amountScalar)
        {
            float chance = Chance * chanceScalar;
            int amount = Mathf.RoundToInt(Random.Range(Amount.x, Amount.y + 1) * amountScalar);
            if (Random.Range(0f, 100f) < chance)
            {
                return new LootElementAmount(amount, Prefab);
            }
            return null;
        }
    }
}
