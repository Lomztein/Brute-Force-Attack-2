using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Colorization
{
    public enum Color
    {
        Blue, Yellow, Red, Purple, Orange, Green, White, Black
    }

    public class ColorInfo
    {
        private static ColorInfo[] _descriptors = new []
        {
            new ColorInfo (Color.Blue, "Blue", "Blue is cold, calm, relaxing. Blue is sadness, blue is dispair.", UnityEngine.Color.cyan),
            new ColorInfo (Color.Yellow, "Yellow", "Yellow is speed, fast and unrelenting. Yellow is happiness, but deciet as well.", UnityEngine.Color.yellow),
            new ColorInfo (Color.Red, "Red", "Red is rage, passion and power. Death will follow where the blood flows.", UnityEngine.Color.red),
            new ColorInfo (Color.Purple, "Purple", "Purple ambition and riches, nobility and power.", UnityEngine.Color.magenta),
            new ColorInfo (Color.Orange, "Orange", "Orange is protecting warmth, sorrounding you as a pleasent aura.", new UnityEngine.Color(1f, 0.5f, 1f)),
            new ColorInfo (Color.Green, "Green", "Green is nature, fresh and vibrant. Jealousy will never gain you anything.", UnityEngine.Color.green),
            new ColorInfo (Color.White, "White", "EMBRACE THE VOID", UnityEngine.Color.white),
            new ColorInfo (Color.Black, "Black", "EMBRACE THE VOID", UnityEngine.Color.black),
        };

        public static ColorInfo Get(Color color)
        {
            return _descriptors.First(x => x.Type == color);
        }

        public ColorInfo (Color type, string name, string description, UnityEngine.Color color)
        {
            Type = type;
            Name = name;
            Description = description;
            DisplayColor = color;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public UnityEngine.Color DisplayColor { get; private set; }
        public Color Type { get; private set; }
    }
}
