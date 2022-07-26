using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    public class AreaBuffAbility : Ability
    {
        [ModelAssetReference]
        public Mod Mod;
        [ModelProperty]
        public float Range;
        [ModelProperty]
        public float Time;
        [ModelProperty]
        public LayerMask Layer;

        public override void Activate(AbilityPlacement placement)
        {
            base.Activate(placement);
            if (placement is ClickAbilityPlacement clickPlacement)
            {
                BuffModBroadcaster.AddBuffsInArea(Mod, clickPlacement.CurrentPosition, Range, Time);
            }
        }
    }
}
