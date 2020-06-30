using Lomztein.BFA2.UI.Style.Stylizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Style
{
    [ExecuteAlways]
    public class UIStyleController : MonoBehaviour
    {
        public UIStyle[] AvailableStyles;
        public int Selection;

        public UIStyle GetCurrentStyle ()
        {
            return AvailableStyles[Selection];
        }

        public void ApplyStyle (UIStyle style)
        {
            IUIStylizer[] elements = GetComponentsInChildren<IUIStylizer>();
            foreach (var element in elements)
            {
                element.ApplyStyle(style);
            }
        }

        public void Update()
        {
            if (Application.isEditor)
            {
                ApplyStyle(GetCurrentStyle());
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleStyle(-1);
            }else if(Input.GetKeyDown(KeyCode.E))
            {
                CycleStyle(1);
            }
        }

        private void CycleStyle (int direction)
        {
            Selection += direction;
            if (Selection >= AvailableStyles.Length)
            {
                Selection = 0;
            }else if (Selection < 0)
            {
                Selection = AvailableStyles.Length - 1;
            }
            ApplyStyle(GetCurrentStyle());
        }
    }
}
