using Lomztein.BFA2.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.UI.Windows
{
    public class WindowManager : MonoBehaviour
    {
        private static WindowManager _instance;

        public DarkOverlay DarkOverlay;
        private List<IWindow> _windows = new List<IWindow>();
        private List<GameObject> _windowObjects = new List<GameObject>();
        private bool _openedThisFrame = false;
        private static Dictionary<Type, int> _maxOfType;

        public static event Action<IWindow> OnWindowOpened;
        public static event Action<IWindow> OnWindowClosed;

        public static IWindow[] CurrentWindows => _instance._windows.ToArray();

        private void Awake()
        {
            _instance = this;
            _maxOfType = new Dictionary<Type, int>();
        }

        public static GameObject OpenWindow (GameObject original)
        {
            IWindow wPrefab = original.GetComponent<IWindow>();
            Type wType = wPrefab.GetType();

            if (GetAmountOfType(wType) >= GetMaxOfType(wType))
            {
                _instance._windows.First(x => x.GetType() == wType).Close();
            }

            GameObject window = Instantiate(original, UIController.Instance.MainCanvas.transform);
            window.transform.SetSiblingIndex(_instance.DarkOverlay.transform.GetSiblingIndex() - 1);
            IWindow w = window.GetComponent<IWindow>();

            _instance._openedThisFrame = true;

            _instance._windows.Add(w);
            _instance._windowObjects.Add(window);

            w.Init();

            w.OnClosed += () =>
            {
                _instance.InternalOnWindowClosed(w, window);
            };

            OnWindowOpened?.Invoke(w);
            return window;
        }

        private static int GetAmountOfType(Type type)
            => _instance._windows.Count(x => x.GetType() == type);

        private static int GetMaxOfType (Type type)
            => _maxOfType.ContainsKey(type) ? _maxOfType[type] : 1;

        public static void SetMaxOfType(Type type, int amount)
        {
            if (_maxOfType.ContainsKey(type)) {
                _maxOfType[type] = amount;
            }
            else
            {
                _maxOfType.Add(type, amount);
            }
        }

        private void InternalOnWindowClosed (IWindow window, GameObject windowObj)
        {
            _windows.Remove(window);
            _windowObjects.Remove(windowObj);
            OnWindowClosed?.Invoke(window);

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

        public static GameObject OpenWindowAboveOverlay (GameObject original)
        {
            GameObject window = OpenWindow(original);
            if (window)
            {
                window.transform.SetSiblingIndex(_instance.DarkOverlay.transform.GetSiblingIndex() + 1);
                _instance.DarkOverlay.FadeIn();
                return window;
            }
            return null;
        }

        private void Update()
        {
            if (!_openedThisFrame && Input.PrimaryDown && !UIUtils.IsOverUI(Mouse.current.position.ReadValue()))
            {
                CloseAllWindows();
            }
            _openedThisFrame = false;
        }

        public static void CloseAllWindows()
        {
            Queue<IWindow> toClose = new Queue<IWindow>(_instance._windows);
            if (toClose.Count > 0)
            {
                while (toClose.Count > 0)
                {
                    toClose.Dequeue().Close();
                }
            }
        }
    }
}