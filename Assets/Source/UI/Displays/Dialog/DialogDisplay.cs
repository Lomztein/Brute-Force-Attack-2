using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Lomztein.BFA2.UI.Displays.Dialog
{
    public class DialogDisplay : MonoBehaviour, IPointerDownHandler
    {
        public static DialogDisplay Instance;

        public Image CharacterAvatar;
        public Text CharacterName;
        public Text Text;

        public float DialogStartWaitTime = 0.75f;
        public float DefaultTime = 0.035f;
        public float FullStopTime = 0.5f;
        public float CommaTime = 0.2f;
        public float DialogEndWaitTime = 3f;
        

        private readonly static char[] FullStopChars = new char[] { '.', '!', '?' };

        private string _currentText;
        private Coroutine _currentCoroutine;
        private DialogNode _currentNode;

        public Transform DialogTransform;
        public Transform DialogOptionsParent;
        public Transform VastNothingness;
        public GameObject DialogOptionPrefab;

        public float LerpSpeed;
        public Vector3 RelativeOpenPosition;
        private Vector3 _closedPosition;

        public bool IsOpen { get; private set; }

        private void Awake()
        {
            Instance = this;
            _closedPosition = transform.position;
        }

        private void Update()
        {
            Vector3 target = IsOpen ? (_closedPosition + RelativeOpenPosition) : _closedPosition;
            transform.position = Vector3.Lerp(transform.position, target, LerpSpeed * Time.deltaTime);
        }

        public static void DisplayDialog (DialogTree tree)
        {
            DisplayDialogNode(tree.Root);
        }

        public static void DisplayDialogNode(DialogNode node)
        {
            EndDialog();
            Instance._currentNode = node;
            Instance._currentCoroutine = Instance.StartCoroutine(Instance.DisplayNodeCoroutine(node));
        }

        public static void EndDialog ()
        {
            if (Instance._currentCoroutine != null)
            {
                Instance.StopCoroutine(Instance._currentCoroutine);
                Instance._currentCoroutine = null;
                Instance._currentNode = null;
            }
            Instance.IsOpen = false;
        }

        private IEnumerator DisplayNodeCoroutine (DialogNode node)
        {
            IsOpen = true;
            Texture2D avatar = node.GetAvatar();
            CharacterAvatar.sprite = Sprite.Create(avatar, new Rect(0f, 0f, avatar.width, avatar.height), Vector2.one / 2f);
            CharacterName.text = node.Character.Name;
            ClearText();

            GenerateButtons(node);

            yield return new WaitForSecondsRealtime(DialogStartWaitTime);
            yield return AnimateText(node.Text);

            if (node.Options.Length == 0)
            {
                yield return new WaitForSecondsRealtime(DialogEndWaitTime);
                IsOpen = false;
            }
        }

        private void GenerateButtons (DialogNode node)
        {
            foreach (Transform child in DialogOptionsParent)
            {
                if (child != VastNothingness)
                {
                    Destroy(child.gameObject);
                }
            }
            foreach (DialogNode.Option option in node.Options)
            {
                GameObject go = Instantiate(DialogOptionPrefab, DialogOptionsParent);
                if (option.HasResult)
                {
                    go.GetComponentInChildren<Button>().onClick.AddListener(option.OnSelected);
                }
                else
                {
                    go.GetComponentInChildren<Button>().onClick.AddListener(EndDialog);
                }
                go.GetComponentInChildren<Text>().text = option.Text;
            }
        }

        private void ClearText ()
        {
            _currentText = string.Empty;
            Text.text = _currentText;
        }

        private IEnumerator AnimateText (string text)
        {
            ClearText();
            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                float time = GetCharacterTime(character);
                _currentText += character;
                Text.text = _currentText;
                yield return new WaitForSecondsRealtime(time);
            }
        }

        public static void InstantFinishCurrentNode()
        {
            Instance.StopCoroutine(Instance._currentCoroutine);
            Instance.Text.text = Instance._currentNode.Text;
        }

        public static bool TryAutoSelectSingleOptionOrEnd ()
        {
            var cur = Instance._currentNode;
            if (cur.Options.Length == 1)
            {
                var option = cur.Options[0];
                if (option.HasResult)
                    option.Result.OnSelected();
                else EndDialog();
                return true;
            }else if (cur.Options.Length == 0)
            {
                EndDialog();
                return true;
            }
            
            return false;
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

        public void OnPointerDown(PointerEventData eventData)
        {
            InstantFinishCurrentNode();
        }
    }
}
