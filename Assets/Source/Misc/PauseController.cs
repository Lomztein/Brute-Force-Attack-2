using Lomztein.BFA2.UI.Menus;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Misc
{
    // TODO: Consider refactoring this so that the PauseController controls time, and the window is merely a facade.
    public class PauseController : MonoBehaviour
    {
        public GameObject PauseMenuPrefeb;
        private PauseMenu _currentPauseMenu;

        private void Start()
        {
            InputMaster master = new InputMaster();
            master.General.CancelPause.performed += OnPause;
            master.General.Enable();
            master.Enable();
        }

        private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Toggle();
        }

        public void Toggle ()
        {
            if (_currentPauseMenu == null)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        public void Pause ()
        {
            if (_currentPauseMenu == null)
            {
                _currentPauseMenu = WindowManager.OpenWindowAboveOverlay(PauseMenuPrefeb).GetComponent<PauseMenu>();
            }
        }

        public void Resume ()
        {
            if (_currentPauseMenu != null)
            {
                _currentPauseMenu.Resume();
            }
        }
    }
}
