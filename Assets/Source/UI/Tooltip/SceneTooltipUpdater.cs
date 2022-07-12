using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class SceneTooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        public LayerMask TargetLayers;

        public IHasToolTip GetTooltip()
        {
            Vector2 position = Input.WorldMousePosition;
            var colliders = Physics2D.OverlapPointAll(position, TargetLayers);
            IEnumerable<IHasToolTip> tooltips = colliders.SelectMany (x => x.GetComponents<IHasToolTip>()).Where(x => x != null);
            return tooltips.FirstOrDefault();
        }
    }
}
