using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class RootModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastOnStart => true;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            return transform.root.GetComponentsInChildren<IModdable>();
        }
    }
}
