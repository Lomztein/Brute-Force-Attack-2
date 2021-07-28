using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies.Loot
{
    [Serializable]
    public class LootItem
    {
        [ModelProperty]
        public ContentPrefabReference Prefab;

        [ModelProperty]
        public Vector2Int WaveMinMax;
        [ModelProperty]
        public Vector2Int AmountMinMax;
        [ModelProperty]
        public Vector2 ChanceMinMax;
        [ModelProperty]
        public AnimationCurve ChanceOverTime;

        public int Roll (float chanceScalar, int wave)
        {
            float pos = (wave - WaveMinMax.x) / (float)(WaveMinMax.y - WaveMinMax.x);
            float chance = Mathf.Lerp(ChanceMinMax.x, ChanceMinMax.y, ChanceOverTime.Evaluate(pos));
            int fraction = Mathf.RoundToInt (1 / chance);

            if (Random.Range(0, Mathf.RoundToInt(fraction / chanceScalar) + 1) == 0)
            {
                return Random.Range(Mathf.RoundToInt(AmountMinMax.x), Mathf.RoundToInt(AmountMinMax.y + 1));
            }
            return 0;
        }
    }
}
