using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.PurchaseHandlers
{
    public abstract class PlacementPurchaseHandler : MonoBehaviour, IPurchaseHandler
    {
        public void Handle(IPurchasable purchasable, IResourceContainer resources)
        {
            GameObject go = (purchasable as Component).gameObject;
            GameObject instance = Instantiate(go);
            ReflectionUtils.DynamicBroadcastInvoke(instance, "OnInstantiated"); // Definitively hacky, but better than immidiate alternative.
            instance.SetActive(true);

            ISimplePlacement placement = GetPlacement(purchasable, resources);
            placement.Pickup(instance);

            placement.OnPlaced += () => resources.TrySpend(purchasable.Cost);
            PlacementController.Instance.TakePlacement(placement);
        }

        public abstract ISimplePlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources);
    }
}
