using Lomztein.BFA2.Placement;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects
{
    public class MapObjectHandle : MonoBehaviour, ITooltip
    {
        private GameObject _object;
        public BoxCollider2D Collider;
        public Bounds Bounds => Collider.bounds;

        public string Title => _object.name;
        public string Description => null;
        public string Footnote => null;

        private bool _pickable = true;

        public void Assign (GameObject obj)
        {
            _object = obj;
            UpdateCollider();
            Init();
        }

        private void Update()
        {
            if (_object)
            {
                _object.transform.position = transform.position;
                _object.transform.rotation = transform.rotation;
            }
        }

        private void UpdateCollider ()
        {
            Renderer ren = _object.GetComponentInChildren<Renderer>() ?? GetComponentInChildren<Renderer>();
            Collider.size = ren.bounds.size;
        }

        private void Click()
        {
            MapObjectPlacement placement = new MapObjectPlacement();
            placement.Pickup(this);

            PlacementController.Instance.TakePlacement(placement);
        }

        private void Init()
        {
            MapEditorController.Instance.AddMapObject(_object);
        }

        public void Delete ()
        {
            if (_object != null)
            {
                _object.transform.SetParent(null);
                Destroy(_object);
            }
            Destroy(gameObject);
        }
    }
}
