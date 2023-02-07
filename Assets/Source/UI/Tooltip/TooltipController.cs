using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class TooltipController : MonoBehaviour
    {
        public static TooltipController Instance { get; private set; }

        public RectTransform TooltipTransform;
        public Vector2 Offset;

        public GameObject[] Updaters;
        private ITooltipUpdater[] _updaters;

        private IHasToolTip _currentToolTip;

        private void Awake()
        {
            _updaters = Updaters.Select(x => x.GetComponent<ITooltipUpdater>()).ToArray();
            Instance = this;
        }

        private void Update()
        {
            bool hasChanged = false;
            bool any = false;

            foreach (ITooltipUpdater updater in _updaters)
            {
                IHasToolTip tooltip = updater.GetTooltip();
                if (tooltip != null)
                {
                    any = true;
                    if (!tooltip.Equals(_currentToolTip))
                    {
                        hasChanged = true;
                        ClearTooltip();
                    }
                    _currentToolTip = tooltip;
                    break;
                }
            }

            if (any == false)
            {
                _currentToolTip = null;
            }

            if (_currentToolTip == null)
            {
                TooltipTransform.gameObject.SetActive(false);
                ClearTooltip();
            }
            else if (hasChanged)
            {
                GameObject newTooltip = _currentToolTip.InstantiateToolTip();
                newTooltip.transform.SetParent(TooltipTransform);
                TooltipTransform.gameObject.SetActive(true);
            }

            Vector2 flip = new Vector2();
            Vector2 pos = MousePosition.ScreenPosition;
            Rect rect = TooltipTransform.rect;

            if (pos.x + rect.width > Screen.width)
            {
                flip.x = -rect.width + Offset.x * -2;
            }
            if (pos.y - rect.height < 0f)
            {
                flip.y = rect.height + Offset.y * 2;
            }

            TooltipTransform.position = pos + Offset + flip;
        }

        public static void ForceResetToolTip ()
        {
            Instance._currentToolTip = null;
        }

        private void ClearTooltip()
        {
            foreach (Transform child in TooltipTransform)
            {
                Destroy(child.gameObject);
            }
        }

        private static bool HasChanged (List<GameObject> prevToolTips, List<GameObject> currentToolTips)
        {
            if (prevToolTips.Count != currentToolTips.Count) return true;

            for (int i = 0; i < prevToolTips.Count; i++)
            {
                if (prevToolTips[i] != currentToolTips[i]) return true;
            }

            return false;
        }
    }
}
