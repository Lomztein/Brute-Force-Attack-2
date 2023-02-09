using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Weaponary.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary
{
    public class DamageInfo
    {
        public object Source;
        public ITarget IntendedTarget;

        public double Damage;
        public Color Color;
        public bool Direct;

        public double DamageDealt { get; set; }

        public DamageInfo(object source, ITarget intendedTarget, double damage, Color color, bool direct)
        {
            Damage = damage;
            Color = color;
            Source = source;
            IntendedTarget = intendedTarget;
            Direct = direct;
        }

        public bool TryGetSource<T>(out T source) where T : class
        {
            source = Source as T;
            return source != null;
        }
    }
}
