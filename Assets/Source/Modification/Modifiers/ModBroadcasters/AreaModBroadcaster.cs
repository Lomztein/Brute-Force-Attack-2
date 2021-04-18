using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
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

        public override IEnumerable<IModdable> GetBroadcastTargets()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, Range, TargetLayer);
            var self = GetComponent<Collider2D>();

            foreach (var collider in colliders)
            {
                if (!IncludeSelf && collider == self)
                    continue;

                IModdable moddable = collider.GetComponent<IModdable>();
                if (moddable != null)
                    yield return moddable;
            }
        }
    }
}
