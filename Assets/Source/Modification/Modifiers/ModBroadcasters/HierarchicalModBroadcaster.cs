using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class HierarchicalModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastOnStart => true;

        [ModelProperty]
        public bool ProvideUpwards;
        [ModelProperty]
        public bool ProvideDownwards;
        [ModelProperty]
        public bool ProvideSelf;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            List<IModdable> moddables = new List<IModdable>();
            IModdable self = GetComponent<IModdable>();

            if (ProvideUpwards)
            {
                IEnumerable<IModdable> upwards = GetComponentsInParent<IModdable>().Where(x => x != self);
                moddables.AddRange(upwards);
            }
            if (ProvideDownwards)
            {
                IEnumerable<IModdable> downwards = GetComponentsInChildren<IModdable>().Where(x => x != self);
                moddables.AddRange(downwards);
            }
            if (ProvideSelf)
            {
                moddables.Add(self);
            }

            return moddables;
        }
    }
}
