using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Loot
{
    public class CompositeLootTable : ILootTable
    {
        private ILootTable[] _tables;

        public CompositeLootTable(params ILootTable[] tables)
        {
            _tables = tables;
        }

        public RandomizedLoot GetRandomLoot(float chanceScalar, float amountScalar)
            => new RandomizedLoot(_tables.SelectMany(x => x.GetRandomLoot(chanceScalar, amountScalar).Elements).ToArray());
    }
}
