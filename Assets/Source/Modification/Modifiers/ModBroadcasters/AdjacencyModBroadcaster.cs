using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.BFA2.Serialization;

namespace Lomztein.BFA2.Modification.Modifiers.ModBroadcasters
{
    public class AdjacencyModBroadcaster : ModBroadcaster
    {
        protected override bool BroadcastPostAssembled => true;

        [ModelProperty]
        public LayerMask AdjacencyCheckLayer;
        [ModelProperty]
        public float AdjacencyCheckRange;

        protected override void Start()
        {
            base.Start();
            DelayedBroadcast();
        }

        protected override IEnumerable<IModdable> GetBroadcastTargets()
        {
            foreach (Transform sibling in GetSiblings())
            {
                if (IsAdjecent(sibling.gameObject))
                {
                    IModdable moddable = sibling.GetComponent<IModdable>();
                    if (moddable != null && _mod.IsCompatableWith(moddable))
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