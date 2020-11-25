using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class ExpansionCardItemPickHandler : MonoBehaviour, IPickHandler<Item>
    {
        public void Handle(Item pick)
        {
            ExpansionCardItem expItem = pick as ExpansionCardItem;
            ExpansionCardPlacement placement = new ExpansionCardPlacement();
            placement.OnPlaced += (go) => OnPlaced(expItem);
            placement.Pickup(expItem.Prefab.Instantiate());
            PlacementController.Instance.TakePlacement(placement);
        }

        private void OnPlaced(Item item)
        {
            GetComponent<IInventory>().RemoveItem(item);
            item.Destroy();
        }
    }
}
