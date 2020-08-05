using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.UI.Menus.PickerMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class ItemPickMenu : PickMenu<Item>
    {
        public string ItemTypeName;

        public override void AddPickables(IEnumerable<Item> pickables)
        {
            var filtered = pickables.Where(x => x.GetType().Name == ItemTypeName);
            base.AddPickables(filtered);
        }

        public override void RemovePickables(IEnumerable<Item> pickables)
        {
            var filtered = pickables.Where(x => x.GetType().Name == ItemTypeName);
            base.RemovePickables(filtered);
        }

        public override void SetPickables(IEnumerable<Item> pickables)
        {
            var filtered = pickables.Where(x => x.GetType().Name == ItemTypeName);
            base.SetPickables(filtered);
        }
    }
}
