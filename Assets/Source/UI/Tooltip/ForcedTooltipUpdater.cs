using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.ToolTip
{
    public class ForcedTooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        private static ForcedTooltip _toolTip;

        public static void SetTooltip (Func<GameObject> callback, object marker)
        {
            ForcedTooltip newToolTip = new ForcedTooltip();
            newToolTip.Marker = marker;
            newToolTip.Callback = callback;
            _toolTip = newToolTip;
        }

        public static void ResetTooltip()
        {
            _toolTip = null;
        }

        public IHasToolTip GetTooltip()
        {
            return _toolTip;
        }

        private class ForcedTooltip : IHasToolTip
        {
            public object Marker;
            public Func<GameObject> Callback;

            public GameObject InstantiateToolTip()
            {
                return Callback();
            }

            public override bool Equals(object obj)
            {
                if (obj is ForcedTooltip tooltip)
                {
                    return tooltip.Marker.Equals(Marker);
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return Marker.GetHashCode();
            }
        }
    }
}
