using Lomztein.BFA2.Abilities.Effects;
using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Assets.Source.Abilities.Effects
{
    public class ResourceGrantEffect : IAbilityEffect
    {
        [ModelProperty]
        public ResourceCost Grant;

        public void Activate(AbilityPlacement placement)
        {
            foreach (var element in Grant.Elements)
            {
                Player.Player.Instance.Earn(element.Resource, element.Value);
            }
        }
    }
}
