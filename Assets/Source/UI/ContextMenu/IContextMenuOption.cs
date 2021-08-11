using System;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public interface IContextMenuOption
    {
        Func<string> Description { get; }
        Func<string> Name { get; }
        Func<Sprite> Sprite { get; }
        Func<Color?> Tint { get; }
        Func<ContextMenu.Side> Side { get; }

        bool Click();
        bool Interactable();
    }
}