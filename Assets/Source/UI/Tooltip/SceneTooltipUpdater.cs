using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class SceneTooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        public LayerMask TargetLayers;

        public string GetTooltip()
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var colliders = Physics2D.OverlapPointAll(position, TargetLayers);
            ITooltip tooltip = null;

            foreach (var collider in colliders)
            {
                tooltip = collider.GetComponent<ITooltip>();
                if (tooltip != null)
                {
                    break;
                }
            }

            return tooltip?.Text;
        }
    }
}
