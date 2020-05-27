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
            IEnumerable<ITooltip> tooltips = colliders.SelectMany (x => x.GetComponents<ITooltip>()).Where(x => x != null);
            return string.Join("\n", tooltips.Select(x => x.Text));
        }
    }
}
