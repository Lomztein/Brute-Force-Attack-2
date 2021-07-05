using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Style.Stylizers
{
    public class UIClampedStylizer : UIStylizerBase<Graphic>
    {
        public UIStyle.Slot Slot;
        [Range(0, 1)]
        public float TintMax;

        public override void ApplyStyle(UIStyle style)
        {
            Color color = style.GetColor(Slot);
            color = new Color(
                Mathf.Max(color.r, 1-TintMax),
                Mathf.Max(color.g, 1-TintMax),
                Mathf.Max(color.b, 1-TintMax),
                color.a
                );
            GetGraphic().color = color;
        }
    }
}
