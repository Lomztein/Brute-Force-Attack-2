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
            if (gameObject.activeSelf)
            {
                OnClosed?.Invoke();
                Destroy(gameObject);
                Resume();
            }
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

        public void Hide()
            => gameObject.SetActive(false);

        public void Unhide()
            => gameObject.SetActive(true);

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