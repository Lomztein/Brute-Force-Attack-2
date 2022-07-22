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
            var transforms = FindAdjacentTransforms(transform, AdjacencyCheckRange, AdjacencyCheckLayer).ToArray();
            foreach (var adjacent in transforms)
            {
                if (adjacent.TryGetComponent(out IModdable moddable))
                {
                    yield return moddable;
                }
            }
        }

        public static IEnumerable<Transform> FindAdjacentTransforms (Transform trans, float range, LayerMask layerMask)
        {
            /* Rules of adjacency:
            R1: Objects must share common ancestor.
            R2: Objects must be on the same level in the hierarchy
            R3: If there are any rotating ancestors between the common, all ancestors between object and roting ancestor must be on local position (0, 0, 0)
            R4: Objects must be within a very close range.
            R5: If objects have no parents, then current ancestor is the scene.
            */

            // Under R4, find all potential targets within range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(trans.position, range, layerMask);
            foreach (var collider in colliders)
            {
                if (trans != collider.transform && TryFindCommonAncestor(trans, collider.transform, out Transform _))
                {
                    yield return collider.transform;
                }
            }
        }

        private static bool TryFindCommonAncestor(Transform rhs, Transform lhs, out Transform common)
        {
            // Under R1 and R2, we can step-by-step move down through parents untill we find the same for both.
            Transform rhsCurrent = rhs;
            Transform lhsCurrent = lhs;

            // Under R3, we can track if any has been off-center, and if they have and we encounter a rotator, then return false.
            bool rhsOffCenter = false;
            bool lhsOffCenter = false;

            // Under R5, we return true if neither has parents, however the common is null.
            if (rhsCurrent.parent == null && lhsCurrent.parent == null)
            {
                common = null;
                return true;
            }

            while (rhsCurrent != lhsCurrent)
            {
                // Under R3m if either is off-center, and we encounter a rotator, then return false.
                if ((rhsOffCenter && rhsCurrent.GetComponent<IRotator>() != null)
                    || (lhsOffCenter && lhsCurrent.GetComponent<IRotator>() != null))
                {
                    common = null;
                    return false;
                }

                if (Vector3.SqrMagnitude(rhsCurrent.localPosition) > 0.1f) rhsOffCenter = true;
                if (Vector3.SqrMagnitude(lhsCurrent.localPosition) > 0.1f) lhsOffCenter = true;

                if (rhsCurrent.parent && lhsCurrent.parent)
                {
                    rhsCurrent = rhsCurrent.parent;
                    lhsCurrent = lhsCurrent.parent;
                }
                else
                {
                    common = null;
                    return false;
                }
            }

            common = rhsCurrent;
            return true;
        }

    }
}