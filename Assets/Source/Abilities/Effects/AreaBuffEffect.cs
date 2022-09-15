using Lomztein.BFA2.Abilities.Effects;
using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Assets.Source.Abilities.Effects
{
    public class AreaBuffEffect : IAbilityEffect
    {
        [ModelAssetReference]
        public Mod Mod;
        [ModelProperty]
        public float Range;
        [ModelProperty]
        public float Time;
        [ModelProperty]
        public LayerMask Layer;

        public void Activate(AbilityPlacement placement)
        {
            BuffModBroadcaster.AddBuffsInArea(Mod, placement.Position, Range, Time);
        }
    }
}
