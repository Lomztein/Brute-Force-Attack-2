using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenuOption : IContextMenuOption
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Sprite { get; private set; }
        private Func<bool> _onClicked;
        private Func<bool> _interactable;

        public ContextMenuOption(string name, string desc, Sprite sprite, Func<bool> onClicked, Func<bool> interactable)
        {
            Name = name;
            Description = desc;
            Sprite = sprite;
            _onClicked = onClicked;
            _interactable = interactable;
        }

        public bool Click() => _onClicked();

        public bool Interactable() => _interactable();
    }
}