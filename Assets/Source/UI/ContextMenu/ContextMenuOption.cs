using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenuOption : IContextMenuOption
    {
        public Func<Sprite> Sprite { get; private set; }
        public Func<Color?> Tint { get; private set; }
        public Func<ContextMenu.Side> Side { get; private set; }
        public Func<GameObject> ToolTip { get; private set; }

        private Func<bool> _onClicked;
        private Func<bool> _interactable;

        public ContextMenuOption(Func<Sprite> sprite, Func<Color?> tint, Func<ContextMenu.Side> side, Func<bool> onClicked, Func<bool> interactable, Func<GameObject> toolTip)
        {
            Sprite = sprite;
            Tint = tint;
            Side = side;
            _onClicked = onClicked;
            _interactable = interactable;
            ToolTip = toolTip;
        }

        public bool Click() => _onClicked();

        public bool Interactable() => _interactable();

        public GameObject GetToolTip()
        {
            return ToolTip();
        }
    }
}