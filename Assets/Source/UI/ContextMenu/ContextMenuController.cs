using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Turrets.Highlighters;
using Lomztein.BFA2.UI.ContextMenu.Menus;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.Windows;
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

        private Collider2D _currentHover;
        private HighlighterCollection _highlighters;

        public void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos, TargetLayer);
            if (colliders.Length > 0)
            {
                Collider2D hover = colliders.First();
                if (hover != _currentHover)
                {
                    OnHoverChanged(hover, _currentHover);
                    _currentHover = hover;
                }

                if (Input.GetMouseButtonDown (0))
                {
                    IEnumerable<IContextMenuOption> providers = colliders.SelectMany(x => x.GetComponents<IContextMenuOptionProvider>()).Distinct().SelectMany(x => x.GetContextMenuOptions());
                    Open(colliders[0].gameObject, providers, Camera.main.WorldToScreenPoint(colliders[0].transform.position));
                }
            }
            else
            {
                if (_currentHover != null)
                {
                    OnHoverChanged(null, _currentHover);
                    _currentHover = null;
                }
            }
        }

        private void OnHoverChanged (Collider2D cur, Collider2D prev)
        {
            if (_highlighters != null)
            {
                _highlighters.EndHighlight();
            }

            if (cur != null)
            {
                _highlighters = HighlighterCollection.Create(cur.transform.root.gameObject);
                _highlighters.Highlight();
                _highlighters.Tint(Color.green);
            }
        }

        private bool HasActiveWindow(GameObject obj) => _activeMenus.ContainsKey(obj) && _activeMenus[obj] != null;
        private void AddActiveWindow(GameObject obj, IContextMenu menu)
                => _activeMenus.Add(obj, menu as Object);
        private void RemoveActiveWindow(GameObject obj) => _activeMenus.Remove(obj);

        private void Open (GameObject obj, IEnumerable<IContextMenuOption> options, Vector2 position)
        {
            if (!HasActiveWindow(obj))
            {
                HighlighterCollection highlighter = HighlighterCollection.Create(obj.transform.root.gameObject);

                highlighter.Highlight();
                highlighter.Tint(Color.green);

                GameObject menuObj = WindowManager.OpenWindow (MenuPrefab);
                menuObj.transform.position = position;

                IContextMenu menu = menuObj.GetComponent<IContextMenu>();
                menu.Open(options);
                AddActiveWindow(obj, menu);

                menu.OnClosed += () =>
                {
                    RemoveActiveWindow(obj);
                    highlighter.EndHighlight();
                };
            }
        }
    }
}