using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies.Loot
{
    [CreateAssetMenu(fileName = "NewLootTable", menuName = "BFA2/Enemies/Loot Table")]
    public class LootTable : ScriptableObject, IEnumerable<LootItem>
    {
        [ModelProperty]
        public string LootPath;
        private List<LootItem> _items = new List<LootItem>();
        public LootItem[] Items => _items.ToArray();
        public int Count => _items.Count;

        public void Init ()
        {
            _items = LoadItems().ToList();
        }

        public RollResult Roll(Enemy enemy, int wave)
        {
            RollResult result = new RollResult();
            foreach (LootItem item in _items)
            {
                var roll = item.RollItem(enemy, wave);
                if (roll != null && roll.Amount > 0)
                {
                    result.Add(roll);
                }
            }
            return result;
        }

        private IEnumerable<LootItem> LoadItems ()
        {
            return Content.GetAll<LootItem>(LootPath);
        }

        public void AddItem(LootItem item) => _items.Add(item);
        public void RemoveItem(LootItem item) => _items.Remove(item);

        public static LootTable Combine (params LootTable[] tables)
        {
            LootTable newTable = ScriptableObject.CreateInstance<LootTable>();
            foreach (LootTable table in tables)
            {
                foreach (LootItem item in table)
                {
                    newTable.AddItem(item);
                }
            }
            return newTable;
        }

        public IEnumerator<LootItem> GetEnumerator()
        {
            return ((IEnumerable<LootItem>)_items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<LootItem>)_items).GetEnumerator();
        }
    }
}
