using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Purchasing.PurchaseHandlers
{
    class GridPlaceableGameObjectPurchaseHandler : MonoBehaviour, IPurchaseHandler
    {
        public bool Handle(IPurchasable purchasable)
        {
            GameObject go = (purchasable as Component).gameObject;
            GameObject instance = Instantiate(go);
            GridPlacement placement = new GridPlacement();
            if (!PlacementController.Instance.PickUp(placement, instance))
            {
                Destroy(instance);
                return false;
            }
            return true;
        }
    }
}
