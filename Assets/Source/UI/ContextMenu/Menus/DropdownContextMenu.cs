using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu.Menus
{
    public class DropdownContextMenu : MonoBehaviour, IContextMenu
    {
        public event Action OnClosed;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
        }

        public void Open(IEnumerable<IContextMenuOption> options)
        {
        }

        public void StickTo(Transform transform)
        {
        }
    }
}