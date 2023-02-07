using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenuOption : IContextMenuOption
    {
        private Func<Sprite> _sprite = () => null;
        private Func<Color?> _tint = () => null;
        private Func<ContextMenu.Side> _side = () => ContextMenu.Side.Right;
        private Func<GameObject> _toolTip = null;
        private Func<GameObject> _submenu = null;

        private Func<bool> _onClicked = null;
        private Func<bool> _interactable = () => true;

        public Sprite Sprite => _sprite();
        public Color? Tint => _tint();
        public ContextMenu.Side Side => _side();
        public bool Interactable => _interactable();

        public bool HasOnClick => _onClicked != null;
        public bool HasSubMenu => _submenu != null;
        public bool HasToolTip => _toolTip != null;

        public ContextMenuOption(Func<Sprite> sprite, Func<ContextMenu.Side> side)
        {
            _sprite = sprite;
            _side = side;
        }

        public ContextMenuOption(Func<Sprite> sprite, Func<ContextMenu.Side> side, Func<bool> onClicked)
        {
            _sprite = sprite;
            _side = side;
            _onClicked = onClicked;
        }

        public ContextMenuOption(Func<Sprite> sprite, Func<ContextMenu.Side> side, Func<bool> onClicked, Func<GameObject> tooltip)
        {
            _sprite = sprite;
            _side = side;
            _onClicked = onClicked;
            _toolTip = tooltip;
        }

        public ContextMenuOption(Func<Sprite> sprite, Func<Color?> tint, Func<ContextMenu.Side> side, Func<bool> onClicked, Func<bool> interactable, Func<GameObject> toolTip)
        {
            _sprite = sprite;
            _tint = tint;
            _side = side;
            _onClicked = onClicked;
            _interactable = interactable;
            _toolTip = toolTip;
        }

        public ContextMenuOption WithOnClick (Func<bool> onClick)
        {
            _onClicked = onClick;
            return this;
        }

        public ContextMenuOption WithTint(Func<Color?> tint)
        {
            _tint = tint;
            return this;
        } 
        public ContextMenuOption WithInteractable(Func<bool> interactable)
        {
            _interactable = interactable;
            return this;
        }

        public ContextMenuOption WithToolTip(Func<GameObject> tooltip)
        {
            _toolTip = tooltip;
            return this;
        }

        public ContextMenuOption WithSubMenu(Func<GameObject> submenu)
        {
            _submenu = submenu;
            return this;
        }

        public bool OnClick() => _onClicked?.Invoke() ?? false;

        public GameObject InstantiateToolTip()
        {
            return _toolTip();
        }

        public GameObject InstantiateSubMenu ()
        {
            return _submenu();
        }
    }
}