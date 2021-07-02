using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    public class DialogDisplay : MonoBehaviour
    {
        public static DialogDisplay Instance;
        public Text Text;

        private const float DefaultTime = 0.035f;
        private const float FullStopTime = 0.5f;
        private const float CommaTime = 0.2f;

        private readonly static char[] FullStopChars = new char[] { '.', '!', '?' };

        private string _currentText;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ShowDialog("Do you even know why you're here? No? Well strap in lad, for this is about to get hot and heavy.\n\nWe're here to kill. In order to kill, you first need to place down turrets, then the turrets shoot the SHIT out of enemies!");
        }

        public static void ShowDialog (string dialog)
        {
            Instance._currentText = string.Empty;
            Instance.StartCoroutine(Instance.AnimateDialog(dialog));
        }

        private IEnumerator AnimateDialog (string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                float time = GetCharacterTime(character);
                _currentText += character;
                Text.text = _currentText;
                yield return new WaitForSecondsRealtime(time);
            }
        }

        private float GetCharacterTime (char character)
        {
            if (FullStopChars.Contains(character))
            {
                return FullStopTime;
            }else if (character == ',')
            {
                return CommaTime;
            }
            return DefaultTime;
        }
    }
}
