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
        [ModelProperty]
        public Vector2 Weights;
        [ModelProperty]
        public AnimationCurve WeightOverWaves;

        public float GetWeight (int wave)
        {
            float t = Mathf.InverseLerp(EarliestWave, LastWave, wave);
            float e = WeightOverWaves.Evaluate(t);
            return Mathf.Lerp(Weights.x, Weights.y, e);
        }
    }
}
