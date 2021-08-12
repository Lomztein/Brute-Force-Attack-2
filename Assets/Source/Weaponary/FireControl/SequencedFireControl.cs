using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Weaponary.FireSynchronization
{
    public class SequencedFireControl : IFireControl
    {
        public SequencedFireControlSynchronizer Controller;

        public SequencedFireControl (SequencedFireControlSynchronizer controller)
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
