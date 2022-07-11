using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class HierachialModBroadcaster : ModBroadcaster
    {
        public enum ProvideBehaviour { None, Single, All }
        protected override bool BroadcastOnStart => true;

        [ModelProperty]
        public ProvideBehaviour ProvideUpwards;
        [ModelProperty]
        public ProvideBehaviour ProvideDownwards;
        [ModelProperty]
        public bool ProvideSelf;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            List<IModdable> moddables = new List<IModdable>();
            IModdable self = GetComponent<IModdable>();

            if (ProvideUpwards == ProvideBehaviour.All)
            {
                IEnumerable<IModdable> upwards = GetComponentsInParent<IModdable>().Where(x => x != self);
                moddables.AddRange(upwards);
            }else if (ProvideUpwards == ProvideBehaviour.Single)
            {
                moddables.Add(transform.parent.GetComponent<IModdable>());
            }

            if (ProvideDownwards == ProvideBehaviour.All)
            {
                IEnumerable<IModdable> upwards = GetComponentsInChildren<IModdable>().Where(x => x != self);
                moddables.AddRange(upwards);
            }
            else if (ProvideDownwards == ProvideBehaviour.Single)
            {
                foreach (Transform child in transform)
                {
                    IModdable childModdable = child.GetComponent<IModdable>();
                    if (childModdable != null)
                    {
                        moddables.Add(childModdable);
                    }
                }
            }

            if (ProvideSelf)
            {
                moddables.Add(self);
            }

            return moddables;
        }
    }
}
