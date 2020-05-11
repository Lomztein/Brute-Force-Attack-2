using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireControl
{
    public interface IFireControl
    {
        void Fire(int amount, float duration, Action<int> callback);
    }
}
