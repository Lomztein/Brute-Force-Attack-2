using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Colorization
{
    public enum Color
    {
        Blue, Yellow, Red, Pink, Magenta, Green, Orange
    }

    public class ColorInfo
    {
        private static ColorInfo[] _descriptors = new []
        {
            new ColorInfo (Color.Blue, "Blue", "Blue is cold, calm, relaxing. Blue is sadness, blue is dispair.", UnityEngine.Color.blue),
            new ColorInfo (Color.Yellow, "Yellow", "Yellow is speed, fast and unrelenting. Yellow is happiness, but deciet as well.", UnityEngine.Color.yellow),
            new ColorInfo (Color.Red, "Red", "Red is rage, passion and power. Death will follow where the blood flows.", UnityEngine.Color.red),
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
