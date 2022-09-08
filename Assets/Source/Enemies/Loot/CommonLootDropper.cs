using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
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
        public const string COMMON_LOOT_PATH = "*/Loot/Tables/Common.json";
        public const int BASE_FRACTION = 1;

        private static LootTable _commonLootTable;

        public float DropRange;

        public void Start()
        {
            RoundController.Instance.OnEnemySpawned += OnEnemySpawned;
        }

        private void OnEnemySpawned(Enemy obj)
        {
            obj.OnKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(Enemy obj)
        {
            if (obj.WaveHandler && obj is Component component)
            {
                RollResult result = GetTable().Roll(obj, obj.WaveHandler.Wave);
                foreach (var pair in result)
                {
                    for (int i = 0; i < pair.Amount; i++)
                    {
                        InstantiateLoot(pair.Prefab, component.transform.position);
                    }
                }
            }
            obj.OnKilled -= OnEnemyKilled;
        }

        private GameObject InstantiateLoot (IContentCachedPrefab prefab, Vector3 position)
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
                var tables = Content.GetAll<LootTable>(COMMON_LOOT_PATH);
                foreach (var table in tables)
                {
                    table.Init();
                }
                _commonLootTable = LootTable.Combine(tables.ToArray());
            }
            return _commonLootTable;
        }
    }
}
