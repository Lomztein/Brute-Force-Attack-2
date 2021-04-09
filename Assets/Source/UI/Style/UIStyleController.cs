using Lomztein.BFA2.Player.Profile;
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
        public static UIStyleController Main;

        private void Awake()
        {
            if (gameObject.name == "Canvas")
            {
                Main = this;
            }
        }

        public UIStyle GetCurrentStyle ()
        {
            return ProfileManager.CurrentProfile.UIStyle;
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
