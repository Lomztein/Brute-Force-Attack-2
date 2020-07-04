using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Style.Stylizers
{
    public class UISelectableStylizer : UIStylizerBase<Selectable>
    {
        public UIStyle.Slot Normal;
        public UIStyle.Slot Highlight;
        public UIStyle.Slot Pressed;
        public UIStyle.Slot Selected;
        public UIStyle.Slot Disabled;

        public override void ApplyStyle(UIStyle style)
        {
            GetGraphic().colors = new ColorBlock()
            {
                normalColor = style.GetColor(Normal),
                highlightedColor = style.GetColor(Highlight),
                pressedColor = style.GetColor(Pressed),
                selectedColor = style.GetColor(Selected),
                disabledColor = style.GetColor(Disabled),
            };
        }
    }
}
