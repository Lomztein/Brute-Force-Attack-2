using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2
{
    public static class Input
    {
        public static InputMaster Master { get; private set; }

        public static Vector3 ScreenMousePosition => MousePosition.ScreenPosition;
        public static Vector3 WorldMousePosition => MousePosition.WorldPosition;

        public static event Action<InputAction.CallbackContext> CancelPause;
        public static event Action<InputAction.CallbackContext> PrimaryClick;
        public static event Action<InputAction.CallbackContext> SecondaryClick;

        private static InputAction _primaryClick;
        private static InputAction _secondaryClick;

        public static InputActionPhase PrimaryPhase => _primaryClick.phase;
        public static InputActionPhase SecondaryPhase => _secondaryClick.phase;

        public static bool PrimaryPerformed => PrimaryPhase == InputActionPhase.Performed;
        public static bool SecondaryPerformed => SecondaryPhase == InputActionPhase.Performed;

        public static bool PrimaryDown => _primaryClick.ReadValue<float>() > 0.5f;
        public static bool SecondaryDown => _secondaryClick.ReadValue<float>() > 0.5f;

        public static void Init ()
        {
            Master = new InputMaster();

            _primaryClick = Master.General.PrimaryClick;
            _secondaryClick = Master.General.SecondaryClick;

            Master.General.CancelPause.performed += OnCancelPause;
            Master.General.PrimaryClick.performed += OnPrimaryClick;
            Master.General.SecondaryClick.performed += OnSecondaryClick;

            EnableAllMaster();
            Master.Enable();
        }

        private static void EnableAllMaster ()
        {
            Master.General.Enable();
            Master.Battlefield.Enable();
            Master.Placement.Enable();
            Master.Camera.Enable();
            Master.GeneralEditor.Enable();
            Master.AssemblyEditor.Enable();
            Master.MapEditor.Enable();
        }

        private static void OnCancelPause(InputAction.CallbackContext context)
        {
            CancelPause?.Invoke(context);
        }

        private static void OnPrimaryClick(InputAction.CallbackContext context)
        {
            PrimaryClick?.Invoke(context);
        }

        private static void OnSecondaryClick(InputAction.CallbackContext context)
        {
            SecondaryClick?.Invoke(context);
        }
    }
}
