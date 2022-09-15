using Lomztein.BFA2.Placement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities.Placements
{
    /// <summary>
    /// Allows the player to click a point on the map, where the ability will then be activated at.
    /// </summary>
    public class ClickAbilityPlacement : AbilityPlacement, ISimplePlacement
    {
        public event Action<GameObject> OnPlaced;

        public bool Flip()
        {
            return false;
        }

        public bool Pickup(GameObject obj)
        {
            return true;
        }

        public bool Place()
        {
            if (TrySpendActivationCost())
            {
                Ability.Activate();
                Finish();
                return true;
            }
            return false;
        }

        public bool ToPosition(Vector2 position)
        {
            Position = position;
            UpdateVisualizer();
            return true;
        }

        public bool ToRotation(Quaternion rotation)
        {
            Rotation = rotation;
            return true;
        }
    }
}
