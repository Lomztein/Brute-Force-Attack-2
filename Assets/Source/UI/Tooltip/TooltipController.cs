using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class TooltipController : MonoBehaviour
    {
        public RectTransform TooltipTransform;
        public Text TooltipTitle;
        public Text TooltipDescription;
        public Text TooltipFooter;
        public Vector2 Offset;

        public GameObject[] Updaters;
        private ITooltipUpdater[] _updaters;

        private void Awake()
        {
            _updaters = Updaters.Select(x => x.GetComponent<ITooltipUpdater>()).ToArray();
            Debug.Log("Tooltip Awake");
        }

        private void Update()
        {
            TooltipTitle.text = string.Empty;
            TooltipDescription.text = string.Empty;
            TooltipFooter.text = string.Empty;

            foreach (ITooltipUpdater updater in _updaters)
            {
                ITooltip tooltip = updater.GetTooltip();
                if (tooltip != null)
                {
                    TooltipTitle.text = tooltip.Title;
                    TooltipDescription.text = tooltip.Description;
                    TooltipFooter.text = tooltip.Footnote;
                    Debug.Log("Tooltip found tooltip: " + tooltip.Title + " and has pos " + TooltipTransform.position);
                    break;
                }
            }

            TooltipTransform.gameObject.SetActive(!string.IsNullOrEmpty(TooltipTitle.text));
            TooltipDescription.gameObject.SetActive(!string.IsNullOrEmpty(TooltipDescription.text));
            TooltipFooter.gameObject.SetActive(!string.IsNullOrEmpty(TooltipFooter.text));

            Vector2 flip = new Vector2();
            Vector2 pos = Mouse.current.position.ReadValue();
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
    }
}
