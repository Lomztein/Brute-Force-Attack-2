using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    [CreateAssetMenu(fileName = "NewGeneratorEnemyData", menuName ="BFA2/Generator Enemy Data")]
    public class GeneratorEnemyData : ScriptableObject
    {
        [ModelProperty]
        public string EnemyIdentifier;
        [ModelProperty]
        public double DifficultyValue;
        [ModelProperty]
        public int EarliestWave;
        [ModelProperty]
        public int LastWave;
    }
}
