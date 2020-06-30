using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Tooltip
{
    public class ForcedTooltipUpdater : MonoBehaviour, ITooltipUpdater
    {
        private static ForcedTooltip _tooltip = new ForcedTooltip();

        public static void SetTooltip (string title, string desc, string footer)
        {
            _tooltip.Title = title;
            _tooltip.Description = desc;
            _tooltip.Footnote = footer;
        }

        public static void ResetTooltip()
        {
            _tooltip.Title = string.Empty;
            _tooltip.Description = string.Empty;
            _tooltip.Footnote = string.Empty;
        }

        public ITooltip GetTooltip()
        {
            return _tooltip.IsEmpty () ? null : _tooltip;
        }

        private class ForcedTooltip : ITooltip
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Footnote { get; set; }

            public bool IsEmpty() => string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(Footnote);
        }
    }
}
