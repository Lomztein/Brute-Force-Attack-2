using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Pooling
{
    public interface IPoolObject
    {
        bool Ready { get; }

        void DisableSelf();
        void EnableSelf();

        void DestroySelf();
    }
}
