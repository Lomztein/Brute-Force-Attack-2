using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSynchronization
{
    public interface IFireSynchronization
    {
        bool TryFire();
    }

    public class NoFireSynchronization : IFireSynchronization
    {
        public bool TryFire() => true;
    }
}
