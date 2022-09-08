using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot.Rules
{
    public class WaveRule : ILootRule
    {
        [ModelProperty]
        public int Wave;
        [ModelProperty]
        public bool AllowLower, AllowExact, AllowHigher;

        public bool Apply(Enemy enemy, int wave, Roll roll)
        {
            return
                (AllowExact && wave == Wave)
                || (AllowLower && wave < Wave)
                || (AllowHigher && wave > Wave);
        }
    }
}
