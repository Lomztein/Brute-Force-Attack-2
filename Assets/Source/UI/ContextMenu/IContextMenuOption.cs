using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public interface IContextMenuOption
    {
        string Description { get; }
        string Name { get; }
        Sprite Sprite { get; }

        bool Click();
        bool Interactable();
    }
}