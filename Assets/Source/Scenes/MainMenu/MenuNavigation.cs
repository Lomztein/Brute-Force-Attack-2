using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.MainMenu
{
    public class MenuNavigation : MonoBehaviour
    {
        public static MenuNavigation Instance;

        private List<MenuWindow> _navigationBackstack = new List<MenuWindow>();
        public MenuWindow MainMenu;

        private Vector3 _targetPosition;
        public float LerpTime;

        public event Action<MenuWindow, MenuWindow> OnWindowChanged;
        private DragListener _dragListener;

        private void Awake()
        {
            Instance = this;
            Home();
        }

        public string GetCurrentPath ()
        {
            return "/" + string.Join("/", _navigationBackstack.Select(x => x.Name)) + "/";
        }

        public void Return ()
        {
            RemoveLast();
            Goto(GetLast(), false);
        }

        private MenuWindow RemoveLast()
        {
            MenuWindow last = _navigationBackstack[_navigationBackstack.Count - 1];
            if (_navigationBackstack.Count > 1)
            {
                _navigationBackstack.RemoveAt(_navigationBackstack.Count - 1);
            }
            return last;
        }

        private MenuWindow GetLast ()
        {
            return _navigationBackstack[_navigationBackstack.Count - 1];
        }

        public void Goto (MenuWindow next, bool addToBackStack)
        {
            MenuWindow prev = _navigationBackstack.LastOrDefault();

            if (addToBackStack)
            {
                _navigationBackstack.Add(next);
            }
            next.SetHeader(GetCurrentPath(), !OnMainMenu());
            MoveTo(next.transform as RectTransform);
            OnWindowChanged?.Invoke(prev, next);
        }

        private void Clear ()
        {
            _navigationBackstack.Clear();
        }

        public void Home ()
        {
            Clear();
            Goto(MainMenu, true);
        }

        public bool OnMainMenu() => _navigationBackstack.Count == 1 && _navigationBackstack.Last() == MainMenu;

        private void MoveTo (RectTransform transform)
        {
            _targetPosition = transform.anchoredPosition;
        }

        private void Update()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, -_targetPosition, LerpTime * Time.deltaTime);
        }
    }
}
