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
        protected override bool BroadcastPostAwake => true;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            if (transform.parent)
            {
                return transform.parent.GetComponentInChildren<IModdable>().ObjectToEnumerable();
            } return new IModdable[0];
        }
    }
}
