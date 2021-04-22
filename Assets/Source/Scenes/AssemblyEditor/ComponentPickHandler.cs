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
        public GameObject AssemblyPrefab;

        public void Handle(IContentCachedPrefab pick)
        {
            if (AssemblyEditorController.Instance.CurrentAsssembly != null)
            {
                var placement = new ComponentPlacement();
                GameObject obj = pick.Instantiate();
                ReflectionUtils.DynamicBroadcastInvoke(obj, "OnInstantiated");
                placement.Pickup(obj);
                PlacementController.Instance.TakePlacement(placement);
            }
            else
            {
                GameObject newAssembly = Instantiate(AssemblyPrefab, Vector3.zero, Quaternion.identity);
                GameObject baseComponent = pick.Instantiate();
                baseComponent.transform.position = newAssembly.transform.position;
                baseComponent.transform.rotation = newAssembly.transform.rotation;
                baseComponent.transform.parent = newAssembly.transform;

                AssemblyEditorController.Instance.SetAssembly(newAssembly.GetComponent<TurretAssembly>());
            }
        }
    }
}
