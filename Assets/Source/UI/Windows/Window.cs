using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lomztein.BFA2.UI.Windows
{
    public class Window : MonoBehaviour, IWindow
    {
        public event Action OnClosed;
        public UnityEvent Closed;

        public bool DefaultCloseBehaviour = true;

        public void Close()
        {
            if (DefaultCloseBehaviour)
            {
                Destroy(gameObject);
                Closed.Invoke();
            }
        }

        public void ForceClose ()
        {
            Destroy(gameObject);
            Closed.Invoke();
        }

        public void Init()
        {
            Closed.AddListener(() => OnClosed?.Invoke());
        }
    }
}
