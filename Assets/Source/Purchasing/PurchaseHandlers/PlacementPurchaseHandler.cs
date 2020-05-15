using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;
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
        public bool Handle(IPurchasable purchasable, IResourceContainer resources)
        {
            GameObject go = (purchasable as Component).gameObject;
            GameObject instance = Instantiate(go);
            instance.SetActive(true);

            IPlacement placement = GetPlacement(purchasable, resources);
            placement.OnPlaced += () => resources.TrySpend(purchasable.Cost);
            if (!PlacementController.Instance.PickUp(placement, instance))
            {
                Destroy(instance);
                return false;
            }
            return true;
        }

        public abstract IPlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources);
    }
}
