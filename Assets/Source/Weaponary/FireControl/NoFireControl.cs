using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireControl
{
    public class NoFireControl : IFireControl
    {
        public void Fire(int amount, float duration, Action<int> callback)
        {
            for (int i = 0; i < amount; i++)
            {
                callback(i);
            }
        }
    }
}
