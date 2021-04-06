using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2
{
    public class TextArrowAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Text Text;
        private string _baseString;

        private const string _baseKey = "{x}";
        public string HoverFormat = "> {x} <";
        public string ClickFormat = ">{x}<";

        private bool _hovering;
        private bool _clicked;

        public void Start()
        {
            _baseString = Text.text;
        }

        private string GetText (string format, string baseText)
        {
            return format.Replace(_baseKey, _baseString);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Text.text = GetText(ClickFormat, _baseString);
            _clicked = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovering = true;
            Text.text = GetText(HoverFormat, _baseString);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovering = false;
            _clicked = false;
            Text.text = _baseString;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_hovering)
            {
                _clicked = false;
                Text.text = GetText(HoverFormat, _baseString);
            }
        }

        public void OnTextUpdated(string newText)
        {
            _baseString = newText;
            if (_hovering)
            {
                if (_clicked)
                {
                    Text.text = GetText(ClickFormat, _baseString);
                }
                else
                {
                    Text.text = GetText(HoverFormat, _baseString);
                }
            }
        }
    }
}
