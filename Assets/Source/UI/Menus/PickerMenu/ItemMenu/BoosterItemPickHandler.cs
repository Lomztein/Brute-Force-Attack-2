using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class BoosterItemPickHandler : MonoBehaviour, IPickHandler<Item>
    {
        public GameObject BoosterPrefab;

        public void Handle(Item pick)
        {
            BoosterItem expItem = pick as BoosterItem;
            BoosterPlacement placement = new BoosterPlacement();

            placement.OnPlaced += (go) => OnPlaced(expItem);

            BoosterModBroadcaster broadcaster = Instantiate(BoosterPrefab).GetComponent<BoosterModBroadcaster>();
            Mod mod = Instantiate(expItem.Mod);
            mod.Coeffecient = expItem.Coeffecient;

            broadcaster.Mod = mod;
            placement.Pickup(broadcaster.gameObject);

            PlacementController.Instance.TakePlacement(placement);
        }

        private void OnPlaced(Item item)
        {
            GetComponent<IInventory>().RemoveItem(item);
            Player.Player.Inventory.RemoveItem(item);
        }
    }
}
