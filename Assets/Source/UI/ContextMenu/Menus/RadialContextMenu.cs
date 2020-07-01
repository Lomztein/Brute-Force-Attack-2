using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ContextMenu.Menus
{
    public class RadialContextMenu : MonoBehaviour, IContextMenu
    {
        public GameObject ButtonPrefab;

        public float ButtonDist;
        public float LerpSpeed;

        private List<Button> _buttons = new List<Button>();
        private List<IContextMenuOption> _options = new List<IContextMenuOption>();

        public event Action OnClosed;

        public void Close()
        {
            ClearButtons();
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void Open(IEnumerable<IContextMenuOption> options)
        {
            _options = options.ToList();
            if (_options.Count == _buttons.Count)
            {
                UpdateButtons(options);
            }
            else
            {
                foreach (var option in _options)
                {
                    CreateButton(option);
                }
                UpdateButtons(options);
            }
        }

        private void ClearButtons ()
        {
            foreach (Button obj in _buttons)
            {
                Destroy(obj.gameObject);
            }
            _buttons.Clear();
        }

        private void CreateButton (IContextMenuOption option)
        {
            GameObject buttObj = Instantiate(ButtonPrefab, transform.position, Quaternion.identity, transform);
            Button button = buttObj.GetComponent<Button>();
            button.onClick.AddListener(() => ClickButton(option));
            _buttons.Add(button);
        }

        private void ClickButton (IContextMenuOption option)
        {
            bool value = option.Click();
            if (value == true)
            {
                Close();
            }
            else
            {
                UpdateButtons(_options);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                LerpButton(_buttons[i].transform, i);
                _buttons[i].interactable = _options[i].Interactable();
            }
        }

        private void UpdateButtons (IEnumerable<IContextMenuOption> options)
        {
            var arr = options.ToArray();
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = arr[i].Sprite;
                _buttons[i].GetComponent<Tooltip.Tooltip>()._Title = arr[i].Name;
                _buttons[i].GetComponent<Tooltip.Tooltip>()._Description = arr[i].Description;
            }
        }

        private void LerpButton (Transform button, int index)
        {
            button.position = Vector2.Lerp(button.position, transform.position + GetButtonTargetPosition(index), LerpSpeed * Time.deltaTime);
        }

        private Vector3 GetButtonTargetPosition (int index)
        {
            float angle = 360 / _buttons.Count * index;
            angle += 360 / _buttons.Count;

            float rads = Mathf.Deg2Rad * angle;
            float x = Mathf.Cos(rads);
            float y = Mathf.Sin(rads);

            return new Vector2(x, y) * ButtonDist;
        }

        public void Init()
        {
        }
    }
}