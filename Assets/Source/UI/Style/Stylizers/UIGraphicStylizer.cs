using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Style.Stylizers
{
    public class UIGraphicStylizer : UIStylizerBase<Graphic>
    {
        public UIStyle.Slot Slot;
        public Color BlendColor = Color.white;

        public override void ApplyStyle(UIStyle style)
        {
            Color color = style.GetColor(Slot);
            GetGraphic().color = color * BlendColor;
        }
    }
}
