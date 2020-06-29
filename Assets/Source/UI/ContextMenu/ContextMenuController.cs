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

        public void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos, TargetLayer);
            if (colliders.Length > 0)
            {
                if (Input.GetMouseButtonDown (0))
                {
                    IEnumerable<IContextMenuOption> providers = colliders.SelectMany(x => x.GetComponents<IContextMenuOptionProvider>()).Distinct().SelectMany(x => x.GetContextMenuOptions());
                    Open(providers, Camera.main.WorldToScreenPoint(colliders[0].transform.position));
                }
            }
        }

        private void Open (IEnumerable<IContextMenuOption> options, Vector2 position)
        {
            GameObject menuObj = Instantiate(MenuPrefab, position, Quaternion.identity, MenuParent);
            IContextMenu menu = menuObj.GetComponent<IContextMenu>();
            menu.Open(options);
        }
    }
}