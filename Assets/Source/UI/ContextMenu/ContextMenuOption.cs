using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenuOption : IContextMenuOption
    {
        public Func<string> Description { get; private set; }
        public Func<string> Name { get; private set; }
        public Func<Sprite> Sprite { get; private set; }
        public Func<Color?> Tint { get; private set; }
        public Func<ContextMenu.Side> Side { get; private set; }

        private Func<bool> _onClicked;
        private Func<bool> _interactable;

        public ContextMenuOption(Func<string> name, Func<string> description, Func<Sprite> sprite, Func<Color?> tint, Func<ContextMenu.Side> side, Func<bool> onClicked, Func<bool> interactable)
        {
            Name = name;
            Description = description;
            Sprite = sprite;
            Tint = tint;
            Side = side;
            _onClicked = onClicked;
            _interactable = interactable;
        }

        public bool Click() => _onClicked();

        public bool Interactable() => _interactable();
    }
}