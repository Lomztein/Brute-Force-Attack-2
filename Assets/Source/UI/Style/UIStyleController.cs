using Lomztein.BFA2.Game;
using Lomztein.BFA2.UI.Style.Stylizers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Style
{
    [ExecuteAlways]
    public class UIStyleController : MonoBehaviour
    {
        public UIStyle GetCurrentStyle ()
        {
            return PlayerProfile.CurrentProfile.Settings.UIStyle;
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
        }
    }
}
