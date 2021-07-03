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
    public class Prompt : MonoBehaviour, IWindow
    {
        private const string RESOURCE_PATH = "UI/Prompts/Prompt";

        public event Action OnClosed;

        public Text MessageText;
        public InputField InputField;
        public Text PlaceholderText;
        public Button AcceptButton;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void SetMessage (string message)
        {
            MessageText.text = Localization.Get(message);
        }

        public void SetPlaceholder(string placeholder)
        {
            PlaceholderText.text = Localization.Get(placeholder);
        }

        public void Init()
        {
            WindowManager.SetMaxOfType(GetType(), 1);
        }

        public static Prompt Open (string message, Action<string> onAccept)
        {
            GameObject prefab = Resources.Load<GameObject>(RESOURCE_PATH);
            Prompt prompt = WindowManager.OpenWindow(prefab).GetComponent<Prompt>();
            prompt.SetMessage(message);
            prompt.AcceptButton.onClick.AddListener(() => onAccept(prompt.InputField.text));
            return prompt;
        }

        public static Prompt Open (string message, string placeholder, Action<string> onAccept)
        {
            Prompt prompt = Open(message, onAccept);
            prompt.SetPlaceholder(placeholder);
            return prompt;
        }
    }
}
