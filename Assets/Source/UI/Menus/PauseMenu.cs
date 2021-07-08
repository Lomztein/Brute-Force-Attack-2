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
            OnClosed?.Invoke();
            Destroy(gameObject);
            Resume();
        }

        public void Init()
        {
            Pause();
        }

        public void Pause ()
        {
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            Time.timeScale = 1f;
        }

        public void Restart ()
        {
            Resume();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit ()
        {
            Resume();
            SceneManager.LoadScene(0);
        }
    }
}