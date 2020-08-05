using Lomztein.BFA2.Inventory.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        private List<Item> _items = new List<Item>();

        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;

        public Item[] GetItems() => _items.ToArray();

        public void AddItem(Item item)
        {
            _items.Add(item);
            OnItemAdded?.Invoke(item);
            item.transform.SetParent(transform);
        }
        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        public IEnumerator<Item> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
    }
}
