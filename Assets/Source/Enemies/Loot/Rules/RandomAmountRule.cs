using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot.Rules
{
    public class RandomAmountRule : ILootRule
    {
        public int Min;
        public int Max;

        public bool Apply(Enemy enemy, int wave, Roll roll)
        {
            roll.Amount = Random.Range(Min, Max + 1);
            return true;
        }
    }
}
