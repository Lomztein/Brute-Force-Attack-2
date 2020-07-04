using Lomztein.BFA2.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.BFA2.Utilities
{
    public class UIUtils
    {
        private static UIUtils _instance;

        private void Start()
        {
            _instance = this;
        }

        public static bool IsOverUI(Vector2 position)
        {
            GraphicRaycaster raycaster = UIController.Instance.GraphicRaycaster;
            EventSystem eventSystem = UIController.Instance.EventSystem;

            PointerEventData data = new PointerEventData(eventSystem)
            {
                position = position
            };

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(data, results);
            return results.Any();
        }
    }
}
