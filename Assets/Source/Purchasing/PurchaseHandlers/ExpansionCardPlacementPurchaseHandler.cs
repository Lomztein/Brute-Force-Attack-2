﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;

namespace Lomztein.BFA2.Purchasing.PurchaseHandlers
{
    public class ExpansionCardPlacementPurchaseHandler : PlacementPurchaseHandler
    {
        public override IPlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources)
        {
            return new ExpansionCardPlacement(() => resources.HasEnough(purchasable.Cost));
        }
    }
}
