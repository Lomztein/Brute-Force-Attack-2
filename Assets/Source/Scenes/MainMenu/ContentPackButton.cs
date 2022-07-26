using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class ContentPackButton : MonoBehaviour
    {
        public IContentPack ContentPack;
        public Toggle Enabled;
        public Button Button;
        public Text Text;

        private Action<IContentPack> _buttonCallback;
        private Func<IContentPack, bool, bool> _toggleCallback;

        public void Assign(IContentPack pack, Action<IContentPack> buttonCallback, Func<IContentPack, bool, bool> toggleCallback)
        {
            ContentPack = pack;
            Text.text = pack.Name;

            _buttonCallback = buttonCallback;
            _toggleCallback = toggleCallback;

            Enabled.isOn = ContentManager.IsContentPackEnabled(pack.GetUniqueIdentifier());

            Button.onClick.AddListener(OnButtonClick);
            Enabled.onValueChanged.AddListener(OnEnabledToggle);
        }

        private void OnButtonClick ()
        {
            _buttonCallback(ContentPack);
        }

        private void OnEnabledToggle (bool enabled)
        {
            Enabled.isOn = _toggleCallback(ContentPack, enabled);
        }
    }
}
