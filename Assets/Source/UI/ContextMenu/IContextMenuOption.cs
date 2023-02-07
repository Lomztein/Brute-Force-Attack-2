using Lomztein.BFA2.UI.ToolTip;
using System;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public interface IContextMenuOption : IHasToolTip
    {
        Sprite Sprite { get; }
        Color? Tint { get; }
        ContextMenu.Side Side { get; }
        bool Interactable { get; }

        bool OnClick();

        public GameObject InstantiateSubMenu();

        bool HasOnClick { get; }
        bool HasSubMenu { get; }
        bool HasToolTip { get; }
    }
}