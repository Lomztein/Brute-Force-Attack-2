using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class MenuWindow : MonoBehaviour
    {
        public string Name;
        public Text HeaderText;
        public Button ReturnButton;

        public MenuNavigation MenuNavigation;

        private void Start()
        {
            if (MenuNavigation == null)
            {
                MenuNavigation = GetComponentInParent<MenuNavigation>();
            }
        }

        public void Return ()
        {
            MenuNavigation.Return();
        }

        public void SetHeader (string text, bool canReturn)
        {
            HeaderText.text = text;
            ReturnButton.interactable = canReturn;
        }
    }
}
