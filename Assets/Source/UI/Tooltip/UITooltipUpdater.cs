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
        public GraphicRaycaster Raycaster;
        public EventSystem EventSystem;

        public ITooltip GetTooltip()
        {
            PointerEventData data = new PointerEventData(EventSystem);
            data.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            Raycaster.Raycast(data, results);

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
