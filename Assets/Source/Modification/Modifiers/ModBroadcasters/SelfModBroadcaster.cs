using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class SelfModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastOnStart => true;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            return new IModdable[] { GetComponent<IModdable>() };
        }
    }
}
