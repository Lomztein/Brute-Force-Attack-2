using Lomztein.BFA2.Placement;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects
{
    public class MapObjectHandle : MonoBehaviour, IContextMenuOptionProvider, IHasToolTip
    {
        private GameObject _object;
        public BoxCollider2D Collider;
        public Bounds Bounds => Collider.bounds;

        private GameObject[] _handles;

        public Sprite SelectSprite;
        public Sprite DeleteSprite;

        public void Assign (GameObject obj, IEnumerable<GameObject> handles)
        {
            _object = obj;
            _handles = handles.ToArray();
            UpdateCollider();
            Init();
            OnDeselected();
        }

        private void Update()
        {
            if (_object)
            {
                transform.position = _object.transform.position;
                transform.rotation = _object.transform.rotation;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnSelected ()
        {
            foreach (GameObject go in _handles)
            {
                go.SetActive(true);
            }
        }

        public void OnDeselected ()
        {
            foreach (GameObject go in _handles)
            {
                go.SetActive(false);
            }
        }

        private void UpdateCollider ()
        {
            Renderer ren = _object.GetComponentInChildren<Renderer>() ?? GetComponentInChildren<Renderer>();
            Collider.size = ren.bounds.size;
        }

        private void Init()
        {
            MapEditorController.Instance.AddMapObject(_object);
        }

        public bool Delete ()
        {
            if (_object != null)
            {
                _object.transform.SetParent(null);
                Destroy(_object);
            }
            Destroy(gameObject);
            return true;
        }

        private bool Select ()
        {
            MapObjectPlacement placement = new MapObjectPlacement();
            placement.Select(this);
            PlacementController.Instance.TakePlacement(placement);
            return true;
        }

        public IEnumerable<IContextMenuOption> GetContextMenuOptions()
        {
            return new IContextMenuOption[]
            {
                new ContextMenuOption(() => SelectSprite, () => UI.ContextMenu.ContextMenu.Side.Left).WithOnClick(Select).WithToolTip(() => SimpleToolTip.InstantiateToolTip($"Select {_object.name}", "Select this object.")),
                new ContextMenuOption(() => DeleteSprite, () => UI.ContextMenu.ContextMenu.Side.Right).WithOnClick(Delete).WithToolTip(() => SimpleToolTip.InstantiateToolTip($"Delete {_object.name}", "Delete this object.")),
            };
        }

        public GameObject InstantiateToolTip()
        {
            if (_object.TryGetComponent(out INamed named))
                return SimpleToolTip.InstantiateToolTip(named.Name, named.Description);
            return SimpleToolTip.InstantiateToolTip(_object.name);
        }
    }
}
