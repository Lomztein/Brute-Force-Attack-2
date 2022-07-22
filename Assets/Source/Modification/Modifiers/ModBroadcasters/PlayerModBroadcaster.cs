using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class PlayerModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastOnStart => true;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            yield return Player.Player.Instance;
        }
    }
}
