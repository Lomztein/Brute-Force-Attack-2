using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class UITooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        public ITooltip GetTooltip()
        {
            GraphicRaycaster raycaster = UIController.Instance.GraphicRaycaster;
            EventSystem eventSystem = UIController.Instance.EventSystem;

            PointerEventData data = new PointerEventData(eventSystem);
            data.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(data, results);

            foreach (RaycastResult result in results)
            {
                ITooltip tooltip = result.gameObject.GetComponent<ITooltip>();
                if (tooltip != null)
                {
                    return tooltip;
                }
            }

            return null;
        }
    }
}
