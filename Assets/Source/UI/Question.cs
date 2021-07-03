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
    public class Question : MonoBehaviour, IWindow
    {
        private const string RESOURCE_PATH = "UI/Prompts/Question";
        private const string OPTION_RESOURCE_PATH = "UI/Prompts/QuestionOptionButton";

        public event Action OnClosed;

        public Text MessageText;
        public Transform OptionParent;

        public void Close()
        {
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Init()
        {
            WindowManager.SetMaxOfType(GetType(), 1);
        }

        private void AssignOptionTo (GameObject button, Option option)
        {
            button.GetComponentInChildren<Text>().text = Localization.Get(option.Title);
            button.GetComponentInChildren<Button>().onClick.AddListener(() => option.OnSelected());
            button.GetComponentInChildren<Button>().onClick.AddListener(Close);
        }

        private GameObject InstantiateQuestionButton (Option option)
        {
            GameObject prefab = Resources.Load<GameObject>(OPTION_RESOURCE_PATH);
            GameObject button = Instantiate(prefab, OptionParent);
            AssignOptionTo(button, option);
            return button;
        }

        public static Question Open (string message, params Option[] options)
        {
            GameObject prefab = Resources.Load<GameObject>(RESOURCE_PATH);
            Question question = WindowManager.OpenWindow(prefab).GetComponent<Question>();
            question.MessageText.text = Localization.Get(message); // fuck it.
            foreach (Option option in options)
            {
                question.InstantiateQuestionButton(option);
            }
            return question;
        }

        public struct Option
        {
            public string Title;
            public Action OnSelected;

            public Option (string title, Action onSelected)
            {
                Title = title;
                OnSelected = onSelected;
            }
        }
    }
}
