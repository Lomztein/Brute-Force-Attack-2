using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSynchronization
{
    public class SequencedFireSynchronization : IFireSynchronization
    {
        public SequencedFireSyncronizationController Controller;

        public SequencedFireSynchronization (SequencedFireSyncronizationController controller)
        {
            Controller = controller;
        }

        public bool TryFire()
        {
            if (Controller.CanFire(this))
            {
                Controller.OnFire(this);
                return true;
            }
            return false;
        }
    }
}
