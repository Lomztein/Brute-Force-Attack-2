using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.PickHandlers
{
    public class StructurePurchaseHandler : PlacementPurchaseHandler
    {
        public override ISimplePlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources)
        {
            var placement = new StructurePlacement(() => resources.HasEnough (purchasable.Cost) ? null : "Not enough resources");
            placement.OnPlaced += OnPlaced;
            return placement;
        }

        private void OnPlaced(GameObject go)
        {
            Structure structure = go.GetComponent<Structure>();
            if (structure != null)
            {
                StructureManager.AddStructure(structure, this);
            }
        }
    }
}
