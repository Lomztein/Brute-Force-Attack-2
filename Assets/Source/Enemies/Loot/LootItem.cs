using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies.Loot.Rules;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Lomztein.BFA2.Enemies.Loot
{
    [CreateAssetMenu(fileName = "New Loot Item", menuName = "BFA2/LootItem")]
    public class LootItem : ScriptableObject
    {
        [ModelProperty]
        public ContentPrefabReference Prefab;
        [ModelProperty, SerializeReference, SR]
        public ILootRule[] Rules;

        public Roll RollItem (Enemy enemy, int wave)
        {
            Roll roll = new Roll(Prefab, 1);
            foreach (var rule in Rules)
            {
                bool success = rule.Apply(enemy, wave, roll);
                if (!success)
                {
                    return null;
                }
            }
            return roll;
        }
    }
}
