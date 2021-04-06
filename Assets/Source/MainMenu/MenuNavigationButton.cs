using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.MainMenu
{
    public class MenuNavigationButton : MonoBehaviour
    {
        public Button Button;
        public MenuWindow MoveToOnClick;
        public MenuNavigation Navigation;

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                Navigation.Goto(MoveToOnClick, true);
            });
        }
    }
}
