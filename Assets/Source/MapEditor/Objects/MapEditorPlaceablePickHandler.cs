using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.MapEditor.Objects;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.UI.PickerMenu.PickHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor
{
    public class MapEditorPlaceablePickHandler : MonoBehaviour, IPickHandler
    {
        public MapObjectHandleProvider HandleProvider;

        public void Handle(IContentCachedPrefab pick)
        {
            GameObject go = pick.Instantiate();
            var handle = HandleProvider.GetHandle(go);
            handle.Assign(go); // Should maybe be assigned by provider?

            MapObjectPlacement placement = new MapObjectPlacement();
            placement.Pickup(handle);

            PlacementController.Instance.TakePlacement(placement);
        }
    }
}
