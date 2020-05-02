using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2
{
    public class Color
    {
        public static Color Blue = new Color("Blue", "Blue is cold, calm, relaxing. Blue is sadness, blue is dispair.", UnityEngine.Color.blue);
        public static Color Yellow = new Color("Yellow", "Yellow is speed, fast and unrelenting. Yellow is happiness, but deciet as well.", UnityEngine.Color.yellow);
        public static Color Red = new Color("Red", "Red is rage, passion and power. Death will follow where the blood flows.", UnityEngine.Color.red);

        public static Color[] Colors => new Color[]
        {
            Blue, Yellow, Red
        };

        public static Color GetColor (string name)
        {
            return Colors.FirstOrDefault(x => x.Name == name);
        }

        public Color (string name, string description, UnityEngine.Color color)
        {
            Name = name;
            Description = description;
            DisplayColor = color;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public UnityEngine.Color DisplayColor { get; private set; }
    }
}
