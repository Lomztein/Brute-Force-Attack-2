using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Health
{
    public class GameOverWindow : MonoBehaviour, IWindow
    {
        public event Action OnClosed;

        public void Close()
        {
            Debug.Log("lol you can't close on death");
        }

        public void Restart ()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MainMenu ()
        {
            SceneManager.LoadScene(0);
        }

        public void HurrDurr ()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
        }
    }
}
