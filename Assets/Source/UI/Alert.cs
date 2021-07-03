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
    public class Alert : MonoBehaviour, IWindow
    {
        private const string RESOURCE_PATH = "UI/Prompts/Alert";
        public event Action OnClosed;

        public Text AlertText;
        public Button AcceptButton;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void SetText(string text)
        {
            AlertText.text = Localization.Get(text);
        }

        public void Init()
        {
            WindowManager.SetMaxOfType(GetType(), 1);
        }

        public static Alert Open (string alertText)
        {
            GameObject prefab = Resources.Load<GameObject>(RESOURCE_PATH);
            Alert alert = WindowManager.OpenWindow(prefab).GetComponent<Alert>();
            alert.SetText(alertText);
            return alert;
        }

        public static Alert Open (string alertText, Action onAccepted)
        {
            Alert alert = Open(alertText);
            alert.AcceptButton.onClick.AddListener(() => onAccepted());
            return alert;
        }
    }
}
