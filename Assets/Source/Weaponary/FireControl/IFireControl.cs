using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireControl
{
    public interface IFireControl
    {
        bool TryFire();
    }

    public class NoFireControl : IFireControl
    {
        public bool TryFire() => true;
    }
}
