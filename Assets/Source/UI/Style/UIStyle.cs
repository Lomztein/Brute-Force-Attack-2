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
    [CreateAssetMenu(fileName = "NewUIStyke", menuName = "BFA2/UI/Style")]
    public class UIStyle : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [TextArea]
        [ModelProperty]
        public string Description;

        [ModelProperty]
        public Color Primary;
        [ModelProperty]
        public Color Secondary;
        [ModelProperty]
        public Color Highlight;
        [ModelProperty]
        public Color Detail;
        [ModelProperty]
        public Color Text;

        public static UIStyle Default() {
            UIStyle style = CreateInstance<UIStyle>();

            style.Name = "Beautiful Blue";
            style.Description = "Blue, or cyan colors heavily reminiciant of chill summer days and colors similar to cyan.";
            style.Primary = new Color(0f, 1f, 1f);
            style.Secondary = new Color(0f, 1f, 0.8f);
            style.Highlight = new Color(0.2f, 1f, 1f);
            style.Detail = new Color(0.1f, 1f, 1f);
            style.Text = new Color(0.2f, 1f, 1f);

            return style;
        }

        public enum Slot { Primary, Secondary, Highlight, Detail, Text }

        public Color GetColor(Slot slot)
        {
            switch (slot)
            {
                case Slot.Primary: return Primary;
                case Slot.Secondary: return Secondary;
                case Slot.Highlight: return Highlight;
                case Slot.Detail: return Detail;
                case Slot.Text: return Text;
                default: return Color.white;
            }
        }
    }
}
