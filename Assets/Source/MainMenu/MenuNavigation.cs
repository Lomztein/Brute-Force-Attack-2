using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.MainMenu
{
    public class MenuNavigation : MonoBehaviour
    {
        public List<MenuWindow> NavigationBackstack;
        public MenuWindow MainMenu;

        private Vector3 _targetPosition;
        public float LerpTime;

        private void Awake()
        {
            Home();
        }

        public string GetCurrentPath ()
        {
            return "/" + string.Join("/", NavigationBackstack.Select(x => x.Name)) + "/";
        }

        public void Return ()
        {
            RemoveLast();
            Goto(GetLast(), false);
        }

        private MenuWindow RemoveLast()
        {
            MenuWindow last = NavigationBackstack[NavigationBackstack.Count - 1];
            if (NavigationBackstack.Count > 1)
            {
                NavigationBackstack.RemoveAt(NavigationBackstack.Count - 1);
            }
            return last;
        }

        private MenuWindow GetLast ()
        {
            return NavigationBackstack[NavigationBackstack.Count - 1];
        }

        public void Goto (MenuWindow next, bool addToBackStack)
        {
            if (addToBackStack)
            {
                NavigationBackstack.Add(next);
            }
            next.SetHeader(GetCurrentPath(), !OnMainMenu());
            MoveTo(next.transform as RectTransform);
        }

        private void Clear ()
        {
            NavigationBackstack.Clear();
        }

        public void Home ()
        {
            Clear();
            Goto(MainMenu, true);
        }

        public bool OnMainMenu() => NavigationBackstack.Count == 1 && NavigationBackstack.Last() == MainMenu;

        private void MoveTo (RectTransform transform)
        {
            _targetPosition = transform.anchoredPosition;
            Debug.Log(_targetPosition);
        }

        private void Update()
        {
            RectTransform rt = transform as RectTransform;
            rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, -_targetPosition, LerpTime * Time.deltaTime);
        }
    }
}
