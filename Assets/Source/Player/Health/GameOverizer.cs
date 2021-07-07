using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class GameOverizer : MonoBehaviour
    {
        public GameObject GameOverWindowPrefab;
        private IHealthContainer _health;

        public void Awake()
        {
            _health = GetComponent<IHealthContainer>();
            _health.OnHealthExhausted += LooseTheGame;
        }

        private void LooseTheGame()
        {
            Debug.Log("You lost the game.");
            WindowManager.OpenWindowAboveOverlay(GameOverWindowPrefab);
        }
    }
}
