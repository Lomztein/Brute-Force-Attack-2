using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.ItemMenu
{
    public class ModuleItemPickHandler : MonoBehaviour, IPickHandler<Item>
    {
        public GameObject ModulePrefab;

        public void Handle(Item pick)
        {
            ModuleItem expItem = pick as ModuleItem;
            ModulePlacement placement = new ModulePlacement();

            placement.OnPlaced += (go) => OnPlaced(expItem);

            RootModBroadcaster broadcaster = Instantiate(ModulePrefab).GetComponent<RootModBroadcaster>();
            TurretAssemblyModule module = broadcaster.GetComponent<TurretAssemblyModule>();

            Mod mod = expItem.Mod;
            module.Item = expItem;
            broadcaster.Mod = mod;
            broadcaster.ModCoeffecient = expItem.Coeffecient;

            placement.Pickup(module.gameObject);

            PlacementController.Instance.TakePlacement(placement);
        }

        private void OnPlaced(Item item)
        {
            GetComponent<IInventory>().RemoveItem(item);
            Player.Player.Inventory.RemoveItem(item);
        }
    }
}
