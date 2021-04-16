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

        public static event Action<InputAction.CallbackContext> CancelPauseStarted;
        public static event Action<InputAction.CallbackContext> PrimaryClickStarted;
        public static event Action<InputAction.CallbackContext> SecondaryClicKStarted;

        public static InputAction CancelPause { get; private set; }
        public static InputAction PrimaryClick { get; private set; }
        public static InputAction SecondaryClick { get; private set; }

        public static InputActionPhase PrimaryPhase => PrimaryClick.phase;
        public static InputActionPhase SecondaryPhase => SecondaryClick.phase;

        public static bool PrimaryPerformed => PrimaryPhase == InputActionPhase.Performed;
        public static bool SecondaryPerformed => SecondaryPhase == InputActionPhase.Performed;

        public static bool PrimaryDown => PrimaryClick.ReadValue<float>() > 0.5f;
        public static bool SecondaryDown => SecondaryClick.ReadValue<float>() > 0.5f;

        public static void Init ()
        {
            Master = new InputMaster();

            PrimaryClick = Master.General.PrimaryClick;
            SecondaryClick = Master.General.SecondaryClick;

            Master.General.CancelPause.started += OnCancelPauseStarted;
            Master.General.PrimaryClick.started += OnPrimaryClickStarted;
            Master.General.SecondaryClick.started += OnSecondaryClickStarted;

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

        private static void OnCancelPauseStarted(InputAction.CallbackContext context)
        {
            CancelPauseStarted?.Invoke(context);
        }

        private static void OnPrimaryClickStarted(InputAction.CallbackContext context)
        {
            PrimaryClickStarted?.Invoke(context);
        }

        private static void OnSecondaryClickStarted(InputAction.CallbackContext context)
        {
            SecondaryClicKStarted?.Invoke(context);
        }
    }
}
