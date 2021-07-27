using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot
{
    public class CommonLootDropper : MonoBehaviour
    {
        public const string COMMON_LOOT_PATH = "*/Loot/Common";
        public const int BASE_FRACTION = 100;

        private static LootTable _commonLootTable;

        public float DropRange;

        public void Start()
        {
            RoundController.Instance.OnEnemyKilled += Instance_OnEnemyKilled;
        }

        private void Instance_OnEnemyKilled(IEnemy obj)
        {
            if (obj.WaveHandler && obj is Component component)
            {
                RollResult result = GetTable().Roll(BASE_FRACTION / (float)obj.WaveHandler.Amount, obj.WaveHandler.Wave);
                foreach (var pair in result)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        InstantiateLoot(pair.Key, component.transform.position);
                    }
                }
            }
        }

        private GameObject InstantiateLoot (ContentPrefabReference prefab, Vector3 position)
        {
            GameObject obj = prefab.Instantiate();
            obj.transform.position = position + (Vector3)UnityEngine.Random.insideUnitCircle * DropRange;
            obj.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
            return obj;
        }

        private static LootTable GetTable ()
        {
            if (_commonLootTable == null)
            {
                _commonLootTable = LootTable.Combine(Content.GetAll<LootTable>(COMMON_LOOT_PATH));
            }
            return _commonLootTable;
        }
    }
}
