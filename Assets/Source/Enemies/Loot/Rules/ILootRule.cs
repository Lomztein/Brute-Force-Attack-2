using Lomztein.BFA2.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot.Rules
{
    public interface ILootRule
    {
        public bool Apply(Enemy enemy, int wave, Roll roll);
    }
}
