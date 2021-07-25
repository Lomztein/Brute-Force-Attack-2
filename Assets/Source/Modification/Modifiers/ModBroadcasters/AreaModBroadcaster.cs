using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class AreaModBroadcaster : ModBroadcaster, IRanger
    {
        [ModelProperty]
        public LayerMask TargetLayer;
        [ModelProperty]
        public float Range;
        [ModelProperty]
        public bool IncludeSelf;

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

        public float GetRange() => Range;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            var parents = StructureManager.GetStructures();
            var moddables = parents.Where(x => x.OverlapsCircle(transform.position, GetRange()))
                .SelectMany(x => x.GetComponentsInChildren<IModdable>());

            foreach (var moddable in moddables)
            {
                if (!IncludeSelf && (UnityEngine.Object)moddable == this)
                    continue;
                
                yield return moddable;
            }
        }
    }
}
