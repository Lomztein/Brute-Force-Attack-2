using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Lomztein.BFA2.UI.Windows
{
    public class WindowOpener : MonoBehaviour
    {
        public GameObject Window;
        public bool AboveOverlay;

        public UnityEvent OnWindowClosed;

        public void Open ()
        {
            IWindow window = null;
            if (AboveOverlay)
            {
                window = WindowManager.OpenWindowAboveOverlay(Window).GetComponent<IWindow>();
            }
            else
            {
                window = WindowManager.OpenWindow(Window).GetComponent<IWindow>();
            }
            window.OnClosed += OnWindowClosed.Invoke;
        }
    }
}
