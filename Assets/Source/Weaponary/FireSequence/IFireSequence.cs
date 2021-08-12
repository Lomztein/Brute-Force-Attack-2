using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSequence
{
    public interface IFireSequence
    {
        void Fire(int amount, float duration, Action<int> callback);
    }
}
