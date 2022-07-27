using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    [CreateAssetMenu(fileName = "New Resource Grant Ability", menuName = "BFA2/Abilities/Resource Grant")]
    public class ResourceGrantAbility : Ability
    {
        [ModelProperty]
        public ResourceCost Grant;

        public override void Activate(AbilityPlacement placement)
        {
            base.Activate(placement);
            foreach (var element in Grant.Elements)
            {
                Player.Player.Instance.Earn(element.Resource, element.Value);
            }
        }

        public override AbilityPlacement GetPlacement()
        {
            return null;
        }
    }
}
