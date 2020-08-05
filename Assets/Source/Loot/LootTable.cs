using Lomztein.BFA2.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Loot
{
    public class LootTable : ILootTable
    {
        private readonly LootElement[] _elements;

        public LootTable(params LootElement[] elements)
        {
            _elements = elements;
        }

        public RandomizedLoot GetRandomLoot(float chanceScalar, float amountScalar)
        {
            List<LootElementAmount> results = new List<LootElementAmount>();
            foreach (var element in _elements)
            {
                LootElementAmount res = element.GetRandomAmount(chanceScalar, amountScalar);
                if (res != null)
                {
                    results.Add(res);
                }
            }
            return new RandomizedLoot(results.ToArray());
        }
    }
}
