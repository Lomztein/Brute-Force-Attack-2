using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Menus.PickerMenu.CachedPrefab.PickHandlers
{
    public abstract class PlacementPurchaseHandler : MonoBehaviour, IPickHandler<IContentCachedPrefab>
    {
        public void Handle(IContentCachedPrefab prefab)
        {
            GameObject go = prefab.GetCache();
            IPurchasable purchasable = go.GetComponent<IPurchasable>();
            IResourceContainer resources = GetComponent<IResourceContainer>();

            if (resources.HasEnough(purchasable.Cost))
            {
                GameObject instance = prefab.Instantiate();
                instance.SetActive(true);

                ISimplePlacement placement = GetPlacement(purchasable, resources);
                placement.Pickup(instance);

                placement.OnPlaced += (obj) => resources.TrySpend(purchasable.Cost);
                PlacementController.Instance.TakePlacement(placement);

                ReflectionUtils.DynamicBroadcastInvoke(instance, "OnInstantiated", true); // Definitively hacky, but better than immidiate alternative.
                ReflectionUtils.DynamicBroadcastInvoke(instance, "OnPostInstantiated", true);
            }
        }

        public abstract ISimplePlacement GetPlacement(IPurchasable purchasable, IResourceContainer resources);
    }
}
