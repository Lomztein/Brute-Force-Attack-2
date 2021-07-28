using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Menus
{
    public class TabMenu : MonoBehaviour
    {
        public GameObject[] Submenus;
        private ITabMenuElement[] _subMenus;
        private int _currentOpen;

        public GameObject TabButtonPrefab;
        public Transform TabButtonParent;
        public bool SelfInit = true;
        private Button[] _tabButtons;

        private void Start()
        {
            if (SelfInit)
            {
                SetSubmenus(Submenus.Select(x => x.GetComponent<ITabMenuElement>()).ToArray());
            }
;        }

        public void SetSubmenus (ITabMenuElement[] submenus)
        {
            _subMenus = submenus;
            BuildButtons();

            if (_subMenus.Length > 0)
            {
                Open(0);
            }
        }

        private void BuildButtons ()
        {
            foreach (Transform child in TabButtonParent)
            {
                Destroy(child.gameObject);
            }
            _tabButtons = new Button[_subMenus.Length];

            for (int i = 0; i < _subMenus.Length; i++)
            {
                GameObject newButton = Instantiate(TabButtonPrefab, TabButtonParent);
                Button button = newButton.GetComponentInChildren<Button>();
                AddButtonListener(button, i);
                newButton.GetComponentInChildren<Text>().text = _subMenus[i].Name;

                _tabButtons[i] = button;
                _subMenus[i].OnNameChanged += UpdateButton;
                _subMenus[i].Init();
            }
        }

        private void UpdateButton (ITabMenuElement submenu)
        {
            int index = Array.IndexOf(_subMenus, submenu);
            _tabButtons[index].GetComponentInChildren<Text>().text = submenu.Name;
        }

        private void AddButtonListener (Button button, int index)
        {
            button.onClick.AddListener(() => Open(index));
        }

        private void Open (int index)
        {
            _subMenus[index].OpenMenu();
            _currentOpen = index;
            CloseOthers(index);
            _tabButtons[index].interactable = false;
        }

        private void Close (int index)
        {
            _subMenus[index].CloseMenu();
            _tabButtons[index].interactable = true;
        }

        private void CloseOthers (int index)
        {
            for (int i = 0; i < _subMenus.Length; i++)
            {
                if (i != index)
                {
                    Close(i);
                }
            }
        }
    }
}