using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.UI.ContextMenu.Menus;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenuController : MonoBehaviour
    {
        public GameObject MenuPrefab;
        public Transform MenuParent;

        public LayerMask TargetLayer;
        private Dictionary<Object, Object> _activeMenus = new Dictionary<Object, Object>();

        public void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos, TargetLayer);
            if (colliders.Length > 0)
            {
                if (Input.GetMouseButtonDown (0))
                {
                    IEnumerable<IContextMenuOption> providers = colliders.SelectMany(x => x.GetComponents<IContextMenuOptionProvider>()).Distinct().SelectMany(x => x.GetContextMenuOptions());
                    Open(colliders[0], providers, Camera.main.WorldToScreenPoint(colliders[0].transform.position));
                }
            }
        }

        private bool HasActiveWindow(Object obj) => _activeMenus.ContainsKey(obj) && _activeMenus[obj] != null;
        private void SetActiveWindow(Object obj, IContextMenu menu)
        {
            if (_activeMenus.ContainsKey(obj))
            {
                _activeMenus[obj] = menu as Object;
            }
            else
            {
                _activeMenus.Add(obj, menu as Object);
            }
        }

        private void Open (Object obj, IEnumerable<IContextMenuOption> options, Vector2 position)
        {
            if (!HasActiveWindow(obj))
            {
                GameObject menuObj = Instantiate(MenuPrefab, position, Quaternion.identity, MenuParent);
                IContextMenu menu = menuObj.GetComponent<IContextMenu>();
                menu.Open(options);
                SetActiveWindow(obj, menu);
            }
        }
    }
}