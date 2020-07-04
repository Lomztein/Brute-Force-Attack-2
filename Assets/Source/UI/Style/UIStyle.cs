using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.UI.Style
{
    [Serializable]
    public class UIStyle
    {
        public string Name;
        [TextArea]
        public string Description;

        public enum Slot { Primary, Secondary, Highlight, Pressed, Disabled, Text }
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
