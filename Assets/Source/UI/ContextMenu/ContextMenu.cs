using Lomztein.BFA2.UI.Style.Stylizers;
using Lomztein.BFA2.UI.ToolTip;
using Lomztein.BFA2.UI.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ContextMenu
{
    public class ContextMenu : MonoBehaviour, IWindow
    {
        public enum Side { Right, Left, }
        public GameObject ButtonPrefab;

        public float ButtonDist;
        public float LerpSpeed;

        private List<Button> _buttons = new List<Button>();
        private List<IContextMenuOption> _options = new List<IContextMenuOption>();
        private Transform _stickTo;

        public event Action OnClosed;

        public void Close()
        {
            ClearButtons();
            Destroy(gameObject);
            OnClosed?.Invoke();
        }

        public void StickTo (Transform transform)
        {
            _stickTo = transform;
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
        }

        private void Update()
        {
            UpdateButtons(_options);
            if (_stickTo)
            {
                transform.position = Camera.main.WorldToScreenPoint(_stickTo.position);
            }
        }

        private void UpdateButtons (IEnumerable<IContextMenuOption> options)
        {
            var arr = options.ToArray();
            Dictionary<Side, int> indexOnSide = new Dictionary<Side, int>();

            for (int i = 0; i < 4; i++)
            {
                indexOnSide.Add((Side)i, 0);
            }

            for (int i = 0; i < _buttons.Count; i++)
            {
                // Not really great to do every frame but there shouldn't be so many that it matters. Optimize if neccesary.
                _buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = arr[i].Sprite();
                _buttons[i].GetComponent<AssignableToolTip>().SetToolTip(arr[i].ToolTip);

                Color? color = arr[i].Tint();
                if (color.HasValue)
                {
                    _buttons[i].transform.GetChild(0).GetComponent<UIGraphicStylizer>().enabled = false;
                    _buttons[i].transform.GetChild(0).GetComponent<Image>().color = color.Value;
                }


                Side side = arr[i].Side();
                int si = indexOnSide[side];

                LerpButton(_buttons[i].transform, si, arr[i].Side());
                indexOnSide[side]++;
                
                _buttons[i].interactable = _options[i].Interactable();
            }
        }

        private void LerpButton (Transform button, int indexOnSide, Side side)
        {
            button.position = Vector2.Lerp(button.position, transform.position + GetButtonTargetPosition(indexOnSide, side), LerpSpeed * Time.deltaTime);
        }

        private Vector3 GetButtonTargetPosition (int indexOnSide, Side side)
        {
            float start = GetStartingAngle(side);
            float end = GetEndingAngle(side);
            float delta = start - end;

            int count = GetAmountOnSide(side);
            float index = (indexOnSide + 1) / (count + 1f);

            float angle = Mathf.Lerp(start, end, index);

            float rads = Mathf.Deg2Rad * angle;
            float x = Mathf.Cos(rads);
            float y = Mathf.Sin(rads);

            return new Vector2(x, y) * ButtonDist;
        }

        private float GetStartingAngle (Side side)
        {
            float baseAngle = (int)side * 180f;
            return baseAngle - 90f;
        }

        private float GetEndingAngle(Side side)
            => GetStartingAngle(side) + 180f;

        private int GetAmountOnSide(Side side) => _options.Count(x => x.Side() == side);

        public void Init()
        {
        }
    }
}