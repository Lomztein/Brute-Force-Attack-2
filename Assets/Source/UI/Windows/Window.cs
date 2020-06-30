using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.UI.Windows
{
    public class Window : MonoBehaviour, IWindow
    {
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}