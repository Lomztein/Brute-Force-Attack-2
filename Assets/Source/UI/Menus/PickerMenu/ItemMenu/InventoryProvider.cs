using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class InventoryProvider : MonoBehaviour, IDynamicProvider<Item>
    {
        public event Action<IEnumerable<Item>> OnAdded;
        public event Action<IEnumerable<Item>> OnRemoved;

        private IInventory _inventory;

        private IInventory GetInventory ()
        {
            if (_inventory == null)
            {
                _inventory = GetComponent<IInventory>();
            }
            return _inventory;
        }

        private void Awake()
        {
            GetInventory().OnItemAdded += OnItemAdded;
            GetInventory().OnItemRemoved += OnItemRemoved;
        }

        private void OnDestroy()
        {
            if (GetInventory() != null)
            {
                GetInventory().OnItemAdded -= OnItemAdded;
                GetInventory().OnItemRemoved -= OnItemRemoved;
            }
        }

        private void OnItemAdded(Item obj)
        {
            OnAdded?.Invoke(new[] { obj });
        }

        private void OnItemRemoved(Item obj)
        {
            OnRemoved?.Invoke(new[] { obj });
        }

        public Item[] Get() => GetInventory().GetItems();
    }
}
