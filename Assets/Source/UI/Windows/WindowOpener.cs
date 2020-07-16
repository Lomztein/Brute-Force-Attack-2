using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Windows
{
    public class WindowOpener : MonoBehaviour
    {
        public GameObject Window;
        public bool AboveOverlay;

        public void Open ()
        {
            if (AboveOverlay)
            {
                WindowManager.OpenWindowAboveOverlay(Window);
            }
            else
            {
                WindowManager.OpenWindow(Window);
            }
        }
    }
}
