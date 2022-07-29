using Lomztein.BFA2.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2
{
    public class CheatWindowCreator : MonoBehaviour
    {
        public GameObject CheatWindowPrefab;
        private InputMaster _master;

        void Awake()
        {
            _master = new InputMaster();
            _master.General.OpenCheatWindow.performed += OpenCheatWindow_performed;
            _master.Enable();
        }

        private void OnDestroy()
        {
            _master.General.OpenCheatWindow.performed -= OpenCheatWindow_performed;
        }

        private void OpenCheatWindow_performed(InputAction.CallbackContext obj)
        {
            if (obj.phase == InputActionPhase.Performed)
            {
                WindowManager.OpenWindowAboveOverlay(CheatWindowPrefab);
            }
        }
    }
}
