using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Waves
{
    [Serializable]
    public class SpawnInterval
    {
        [ModelProperty]
        public float StartTime;
        [ModelProperty]
        public float Length;
        public float EndTime => StartTime + Length;

        [ModelProperty]
        public string EnemyIdentifier;
        [ModelProperty]
        public int Amount;

        public SpawnInterval() { }

        public SpawnInterval(float start, float length, string identifier, int amount)
        {
            StartTime = Mathf.Max(start, 0f);
            Length = length;
            EnemyIdentifier = identifier;
            Amount = amount;
        }
    }
}
