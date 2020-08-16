using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events.EventArgs
{
    public class HitEventArgs : IEventArgs
    {
        public HitInfo Info;

        public HitEventArgs (HitInfo info)
        {
            Info = info;
        }
    }
}
