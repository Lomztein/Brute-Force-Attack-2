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
    public class PlayerInventoryLink : MonoBehaviour, IInventory
    {
        private IInventory Cache => Player.Player.Inventory;

        public event Action<Item> OnItemAdded {
            add {
                Cache.OnItemAdded += value;
            }

            remove {
                Cache.OnItemAdded -= value;
            }
        }

        public event Action<Item> OnItemRemoved {
            add {
                Cache.OnItemRemoved += value;
            }

            remove {
                Cache.OnItemRemoved -= value;
            }
        }

        public void AddItem(Item item)
        {
            Cache.AddItem(item);
        }

        public Item[] GetItems()
        {
            return Cache.GetItems();
        }

        public void RemoveItem(Item item)
        {
            Cache.RemoveItem(item);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return Cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Cache.GetEnumerator();
        }
    }
}
