﻿using Lomztein.BFA2.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Windows
{
    public class WindowManager : MonoBehaviour
    {
        private static WindowManager _instance;

        public DarkOverlay DarkOverlay;
        private List<IWindow> _windows = new List<IWindow>();
        private List<GameObject> _windowObjects = new List<GameObject>();

        private void Awake()
        {
            _instance = this;
        }

        public static GameObject OpenWindow (GameObject original)
        {
            GameObject window = Instantiate(original, UIController.Instance.MainCanvas.transform);
            window.transform.SetSiblingIndex(_instance.DarkOverlay.transform.GetSiblingIndex() - 1);

            IWindow w = window.GetComponent<IWindow>();
            w.Init();
            _instance.StartCoroutine(_instance.DelayedAdd(w, window));

            w.OnClosed += () =>
            {
                _instance.OnWindowClosed(w, window);

            };

            return window;
        }

        private void OnWindowClosed (IWindow window, GameObject windowObj)
        {
            _windows.Remove(window);
            _windowObjects.Remove(windowObj);

            CheckDarkOverlay();
        }
        
        private void CheckDarkOverlay ()
        {
            foreach (GameObject wobj in _windowObjects)
            {
                if (wobj.transform.GetSiblingIndex() > DarkOverlay.transform.GetSiblingIndex())
                {
                    return;
                }
            }
            DarkOverlay.FadeOut();
        }

        private IEnumerator DelayedAdd (IWindow window, GameObject windowObj)
        {
            yield return new WaitForEndOfFrame();
            _instance._windows.Add(window);
            _instance._windowObjects.Add(windowObj);
        }

        public static GameObject OpenWindowAboveOverlay (GameObject original)
        {
            GameObject window = OpenWindow(original);
            window.transform.SetSiblingIndex(_instance.DarkOverlay.transform.GetSiblingIndex() + 1);
            _instance.DarkOverlay.FadeIn();
            return window;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !UIUtils.IsOverUI(Input.mousePosition))
            {
                CloseAllWindows();
            }
        }

        public static void CloseAllWindows()
        {
            if (_instance._windows.Count > 0)
            {
                while (_instance._windows.Count > 0)
                {
                    _instance._windows[0].Close();
                }
            }
        }

        private IWindow[] GetAllWindows()
            => _windows.ToArray();
    }
}