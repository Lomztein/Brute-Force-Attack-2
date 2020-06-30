using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Windows
{
    public class WindowManager : MonoBehaviour
    {
        public void CloseAllWindows()
        {
            foreach (var window in GetAllWindows())
            {
                window.Close();
            }
        }

        private IWindow[] GetAllWindows()
            => GetComponentsInChildren<IWindow>();
    }
}