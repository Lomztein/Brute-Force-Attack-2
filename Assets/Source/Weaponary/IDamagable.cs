using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary
{
    public interface IDamagable
    {
        double TakeDamage(DamageInfo damageInfo);
    }
}
