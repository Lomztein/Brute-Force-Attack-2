using Lomztein.BFA2.UI.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    public class UIStyleButton : MonoBehaviour
    {
        public Button Button;

        public Text Name;
        public Image Primary;
        public Image Secondary;
        public Image Highlight;
        public Image Detail;
        public Image Text;
        public Image Contrast;

        private UIStyle _style;
        private Action<UIStyle> _callback;

        public void Assign(UIStyle style, Action<UIStyle> callback)
        {
            _style = style;
            Name.text = _style.Name;
            Primary.color = _style.Primary;
            Secondary.color = _style.Secondary;
            Highlight.color = _style.Highlight;
            Detail.color = _style.Detail;
            Text.color = _style.Text;
            Contrast.color = _style.Contrast;

            _callback = callback;

            Button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick ()
        {
            _callback(_style);
        }
    }
}
