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
        protected override bool BroadcastOnStart => true;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
            => transform.parent.GetComponentInChildren<IModdable>().ObjectToEnumerable();
    }
}
