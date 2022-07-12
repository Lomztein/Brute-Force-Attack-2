using Lomztein.BFA2.UI.ToolTip;
using System;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public interface IContextMenuOption : IHasToolTip
    {
        Func<Sprite> Sprite { get; }
        Func<Color?> Tint { get; }
        Func<ContextMenu.Side> Side { get; }
        Func<GameObject> ToolTip { get; }

        bool Click();
        bool Interactable();
    }
}