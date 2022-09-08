using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot.Rules
{
    public class ExternalTableRule : ILootRule
    {
        [ModelAssetReference]
        public LootTable Table;
        private bool _initted;

        public bool Apply(Enemy enemy, int wave, Roll roll)
        {
            if (!_initted) Table.Init();
            _initted = true;
            var result = Table.Roll(enemy, wave);
            var first = result.FirstOrDefault();
            if (first != null)
            {
                roll.Prefab = first.Prefab;
                roll.Amount = first.Amount;
                return true;
            }
            return false;
        }
    }
}
