using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Turrets;
using Lomztein.BFA2.UI.Menus.PickerMenu;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.AssemblyEditor
{
    public class ComponentPickHandler : MonoBehaviour, IPickHandler<IContentCachedPrefab>
    {
        public void Handle(IContentCachedPrefab pick)
        {
            if (!AssemblyEditorController.Instance.IsTierEmpty(AssemblyEditorController.Instance.WorkingTier))
            {
                var placement = new ComponentPlacement();
                GameObject obj = pick.Instantiate();
                ReflectionUtils.DynamicBroadcastInvoke(obj, "OnInstantiated", true);
                placement.Pickup(obj);
                PlacementController.Instance.TakePlacement(placement);
                placement.OnPlaced += OnPlaced;
            }
            else
            {
                Transform tier = AssemblyEditorController.Instance.GetWorkingTierParent();
                GameObject baseComponent = pick.Instantiate();
                baseComponent.transform.position = tier.position;
                baseComponent.transform.rotation = tier.rotation;
                baseComponent.transform.parent = tier;
            }
        }

        private void OnPlaced(GameObject obj)
        {
        }
    }
}
