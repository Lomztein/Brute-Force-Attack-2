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

        public float DialogStartWaitTime = 0.75f;
        public float DefaultTime = 0.035f;
        public float FullStopTime = 0.5f;
        public float CommaTime = 0.2f;
        public float DialogEndWaitTime = 3f;
        

        private readonly static char[] FullStopChars = new char[] { '.', '!', '?' };

        private string _currentText;

        public Transform DialogTransform;

        public float LerpSpeed;
        public Vector3 RelativeOpenPosition;
        private Vector3 _closedPosition;
        private bool _open;

        private void Awake()
        {
            Instance = this;
            _closedPosition = transform.position;
        }

        private void Update()
        {
            Vector3 target = _open ? (_closedPosition + RelativeOpenPosition) : _closedPosition;
            transform.position = Vector3.Lerp(transform.position, target, LerpSpeed * Time.deltaTime);
        }

        public static void ShowDialog (string dialog)
        {
            Instance._currentText = string.Empty;
            Instance.StartCoroutine(Instance.AnimateDialog(dialog));
        }

        private IEnumerator AnimateDialog (string text)
        {
            _currentText += string.Empty;
            Text.text = _currentText;

            _open = true;
            yield return new WaitForSecondsRealtime(DialogStartWaitTime);
            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                float time = GetCharacterTime(character);
                _currentText += character;
                Text.text = _currentText;
                yield return new WaitForSecondsRealtime(time);
            }
            yield return new WaitForSecondsRealtime(DialogEndWaitTime);
            _open = false;
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
