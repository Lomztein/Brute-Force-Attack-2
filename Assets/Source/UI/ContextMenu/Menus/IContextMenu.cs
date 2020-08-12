using Lomztein.BFA2.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu.Menus
{
    public interface IContextMenu : IWindow
    {
        void Open(IEnumerable<IContextMenuOption> options);

        void StickTo(Transform transform); // Potentially split into StickToWorld and StickToScreen? Perhaps split that into a different behaviour?
    }
}