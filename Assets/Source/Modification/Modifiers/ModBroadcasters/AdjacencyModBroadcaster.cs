using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.BFA2.Serialization;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class AdjacencyModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastOnStart => true;
        protected override bool BroadcastPostAwake => true;

        [ModelProperty]
        public LayerMask AdjacencyCheckLayer;
        [ModelProperty]
        public float AdjacencyCheckRange;

        public override IEnumerable<IModdable> GetPotentialBroadcastTargets()
        {
            foreach (Transform sibling in GetSiblings())
            {
                if (IsAdjecent(sibling.gameObject))
                {
                    IModdable moddable = sibling.GetComponent<IModdable>();
                    if (moddable != null && Mod.CanMod(moddable))
                    {
                        yield return moddable;
                    }
                }
            } 
        }

        private IEnumerable<Transform> GetSiblings ()
        {
            if (transform.parent)
            {
                List<Transform> siblings = new List<Transform>(transform.parent.childCount);
                foreach (Transform child in transform.parent)
                {
                    if (child != transform)
                    {
                        yield return child;
                    }
                }
            }
        }

        private bool IsAdjecent(GameObject go) // Terribly wasteful in terms of performance, but easy and should be infrequent enough not to matter.
            => Physics2D.OverlapCircleAll(transform.position, AdjacencyCheckRange, AdjacencyCheckLayer).Any(x => x.gameObject == go);
    }
}