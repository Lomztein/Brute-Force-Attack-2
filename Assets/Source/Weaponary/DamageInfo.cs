using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary
{
    public class DamageInfo
    {
        public float Damage;
        public Color Color;

        public float DamageDealt { get; set; }

        public DamageInfo(float damage, Color color)
        {
            Damage = damage;
            Color = color;
        }
    }
}
