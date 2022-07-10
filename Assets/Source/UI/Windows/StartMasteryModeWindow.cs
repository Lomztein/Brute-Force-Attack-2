using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class StartMasteryModeWindow : MonoBehaviour, IWindow
    {
        public event Action OnClosed;

        public void Close()
        {
            Debug.Log("You must make a choice.");
        }

        private void ActuallyClose()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
        }

        public void BringItOn()
        {
            MasteryModeController.Instance.IncrementMasteryMode();
            ActuallyClose();
        }

        public void MainMenu()
        {
            MasteryModeController.Instance.ReturnToMainMenu();
            ActuallyClose();
        }
    }
}
