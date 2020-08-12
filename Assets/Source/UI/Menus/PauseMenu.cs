using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.UI.Menus
{
    public class PauseMenu : MonoBehaviour, IWindow
    {
        public event Action OnClosed;

        public void Close()
        {
            Time.timeScale = 1f;
            OnClosed?.Invoke();
            Destroy(gameObject);
        }

        public void Init()
        {
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Close();
        }

        public void Restart ()
        {
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit ()
        {
            Close();
            SceneManager.LoadScene(0);
        }
}
}