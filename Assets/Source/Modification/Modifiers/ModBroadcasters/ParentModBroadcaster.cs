using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class ParentModBroadcaster : ModBroadcaster
    {
        public override IEnumerable<IModdable> GetBroadcastTargets()
            => transform.parent.GetComponentInChildren<IModdable>().ObjectToEnumerable();

        protected override void Start()
        {
            base.Start();
            DelayedBroadcast();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ClearMod();
        }
    }
}
