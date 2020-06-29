using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu.Menus
{
    public interface IContextMenu
    {
        void Open(IEnumerable<IContextMenuOption> options);

        void Close();
    }
}