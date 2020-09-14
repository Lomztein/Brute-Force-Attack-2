using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Lomztein.BFA2.UI.Style
{
    [Serializable]
    public class UIStyle
    {
        [ModelProperty]
        public string Name;
        [TextArea]
        [ModelProperty]
        public string Description;

        public static UIStyle Default = new UIStyle()
        {
            Name = "Friendly Cyan",
            Description = "Cyan colors heavily reminiciant of chill summer days and colors similar to cyan.",
            _colors = new Color[]
            {
                new Color(0f, 1f, 1f),
                new Color(0f, 1f, 0.8f),
                new Color(0.2f, 1f, 1f),
                new Color(0f, 0.7f, 0.7f),
                new Color(0f, 0.3f, 0.3f),
                new Color(0.2f, 1f, 1f),
            }
        };

        public enum Slot { Primary, Secondary, Highlight, Pressed, Disabled, Text }
        [ModelProperty]
        [SerializeField] private Color[] _colors;

        public Color GetColor(Slot slot)
        {
            if (_colors.Length <= (int)slot)
            {
                return Color.white;
            }
            else
            {
                return _colors[(int)slot];
            }
        }
    }
}
