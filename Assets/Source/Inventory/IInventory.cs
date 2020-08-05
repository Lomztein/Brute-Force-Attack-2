using Lomztein.BFA2.Inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lomztein.BFA2.Inventory
{
    public interface IInventory : IEnumerable<Item>
    {
        event Action<Item> OnItemAdded;
        event Action<Item> OnItemRemoved;

        void AddItem(Item item);
        Item[] GetItems();
        void RemoveItem(Item item);
    }

    public static class InventoryExtensions
    {
        public static T[] GetItems<T>(this IInventory inventory) where T : Item
            => inventory.Where(x => x is T).Where(x => x != null).Cast<T>().ToArray();

        public static Item[] GetItems(this IInventory inventory, Predicate<Item> predicate)
            => inventory.Where(x => predicate(x)).ToArray();
    }
}