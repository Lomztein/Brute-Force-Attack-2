using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Cheats
{
    public class CheatWindow : MonoBehaviour, IWindow
    {
        public event Action OnClosed;
        public InputField Input;
        private InputMaster _master;

        public void Close()
        {
            OnClosed?.Invoke();
            Destroy(gameObject);
            _master.General.Enter.performed -= Enter_performed;
        }

        public void Init()
        {
            Input.Select();
            _master = new InputMaster();
            _master.General.Enter.performed += Enter_performed;
            _master.Enable();
        }

        private void Enter_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (!string.IsNullOrEmpty(Input.text))
            {
                Accept();
            }
            else
            {
                Close();
            }
        }

        public void Accept ()
        {
            string text = Input.text;
            try
            {
                if (CheatCode.TryExecuteCheat(text))
                {
                    Message.Send($"Activated cheat '{text}'", Message.Type.Minor);
                }
                else
                {
                    Message.Send($"Cheat execution failed. No '{text}' cheat found.", Message.Type.Minor);
                }
            } catch (Exception e)
            {
                Message.Send($"Tried to activate cheat '{text}', but an exception '{e.Message}' occured :(", Message.Type.Minor);
            }
            Close();
        }
    }
}
