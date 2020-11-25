using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class SelfModBroadcaster : ModBroadcaster
    {
        private IModdable _moddable;

        protected override void Start()
        {
            _moddable = GetComponent<IModdable>();
            DelayedBroadcast();
        }

        protected override IEnumerable<IModdable> GetBroadcastTargets()
        {
            return new IModdable[] { _moddable };
        }

        protected override void OnDestroy()
        {
            ClearMod();
        }
    }
}
