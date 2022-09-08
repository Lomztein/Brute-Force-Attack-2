using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot.Rules
{
    public class ProportionalChanceRule : ILootRule
    {
        [ModelProperty]
        public float Denominator;

        public bool Apply(Enemy enemy, int wave, Roll roll)
        {
            float scaler = 1f / RoundController.Instance.GetWave(wave).Amount;
            if (Random.Range(0, Mathf.CeilToInt(Denominator / scaler)) == 0)
            {
                return true;
            }
            return false;
        }
    }
}
