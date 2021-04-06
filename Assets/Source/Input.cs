using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2
{
    public class Input : MonoBehaviour, InputMaster.IGeneralActions
    {
        public static Input Instance { get; private set; }
        public static InputMaster Master { get; private set; }

        public static Vector2 MousePosition { get; private set; }
        public static Vector2 WorldMousePosition { get; private set; }

        public static event Action<InputAction.CallbackContext> CancelPause;
        public static event Action<InputAction.CallbackContext> PrimaryClick;
        public static event Action<InputAction.CallbackContext> SecondaryClick;

        private InputAction _primaryClick;
        private InputAction _secondaryClick;

        public static InputActionPhase PrimaryPhase => Instance._primaryClick.phase;
        public static InputActionPhase SecondaryPhase => Instance._secondaryClick.phase;

        public static bool PrimaryPerformed => PrimaryPhase == InputActionPhase.Performed;
        public static bool SecondaryPerformed => SecondaryPhase == InputActionPhase.Performed;

        public static bool PrimaryDown => Instance._primaryClick.ReadValue<float>() > 0.5f;
        public static bool SecondaryDown => Instance._secondaryClick.ReadValue<float>() > 0.5f;

        public Camera WorldCamera;

        private void Awake ()
        {
            Instance = this;
            Master = new InputMaster();
            Master.General.SetCallbacks(this);

            _primaryClick = Master.General.PrimaryClick;
            _secondaryClick = Master.General.SecondaryClick;

            EnableAllMaster();
            Master.Enable();

            if (WorldCamera == null)
            {
                WorldCamera = Camera.main;
            }
        }

        private void EnableAllMaster ()
        {
            Master.General.Enable();
            Master.Battlefield.Enable();
            Master.Placement.Enable();
            Master.Dragging.Enable();
            Master.Camera.Enable();
            Master.GeneralEditor.Enable();
            Master.AssemblyEditor.Enable();
            Master.MapEditor.Enable();
        }

        private void Update()
        {
            MousePosition = Mouse.current.position.ReadValue();
            WorldMousePosition = WorldCamera.ScreenToWorldPoint(MousePosition);
        }

        public void OnCancelPause(InputAction.CallbackContext context)
        {
            CancelPause?.Invoke(context);
        }

        public void OnPrimaryClick(InputAction.CallbackContext context)
        {
            PrimaryClick?.Invoke(context);
        }

        public void OnSecondaryClick(InputAction.CallbackContext context)
        {
            SecondaryClick?.Invoke(context);
        }
    }
}
