using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class UITooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        public IHasToolTip GetTooltip()
        {
            GraphicRaycaster raycaster = UIController.Instance.GraphicRaycaster;
            EventSystem eventSystem = UIController.Instance.EventSystem;

            PointerEventData data = new PointerEventData(eventSystem);
            data.position = Mouse.current.position.ReadValue();

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(data, results);

            foreach (RaycastResult result in results)
            {
                IHasToolTip tooltip = result.gameObject.GetComponent<IHasToolTip>();
                if (tooltip != null)
                {
                    return tooltip;
                }
            }

            return null;
        }
    }
}
