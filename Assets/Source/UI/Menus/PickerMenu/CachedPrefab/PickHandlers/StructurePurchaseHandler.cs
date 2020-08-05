using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.PickHandlers
{
    public class StructurePurchaseHandler : PlacementPurchaseHandler
    {
        public override ISimplePlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources)
        {
            return new StructurePlacement(() => resources.HasEnough (purchasable.Cost) ? null : "Not enough resources");
        }
    }
}
