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

        public Vector2 CurrentPosition { get; private set; }

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
            Ability.Activate(this);
            Finish();
            return true;
        }

        public bool ToPosition(Vector2 position)
        {
            CurrentPosition = position;
            UpdateVisualizer();
            return true;
        }

        public bool ToRotation(Quaternion rotation)
        {
            return true;
        }
    }
}
