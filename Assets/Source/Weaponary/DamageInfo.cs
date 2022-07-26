using Lomztein.BFA2.Colorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary
{
    public class DamageInfo
    {
        public double Damage;
        public Color Color;

        public double DamageDealt { get; set; }

        public DamageInfo(double damage, Color color)
        {
            Damage = damage;
            Color = color;
        }
    }
}
