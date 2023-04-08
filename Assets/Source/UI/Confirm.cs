using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    public class Confirm : MonoBehaviour, IWindow
    {
        private const string RESOURCE_PATH = "UI/Prompts/Confirm";
        public event Action OnClosed;

        public Text MessageText;
        public Button ConfirmButton;
        public Button CancelButton;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void SetText(string text)
        {
            MessageText.text = Localization.Get(text);
        }

        public void Init()
        {
            WindowManager.SetMaxOfType(GetType(), 1);
        }

        public static Confirm Open (string message, Action onConfirm)
        {
            GameObject prefab = Resources.Load<GameObject>(RESOURCE_PATH);
            var confirmObj = WindowManager.OpenWindowAboveOverlay(prefab);
            if (confirmObj.TryGetComponent(out Confirm confirm))
            {
                confirm.MessageText.text = message;
                confirm.ConfirmButton.onClick.AddListener(() => onConfirm());
                return confirm;
            }
            return null;
        }

        public static Confirm Open (string message, Action onConfirm, Action onCancel)
        {
            Confirm confirm = Open(message, onConfirm);
            confirm.CancelButton.onClick.AddListener(() => onCancel());
            return confirm;
        }
    }
}
