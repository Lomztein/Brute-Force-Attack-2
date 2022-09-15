using Lomztein.BFA2.Abilities.Placements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities.Visualizers
{
    public class PositionAbilityVisualizer : AbilityVisualizer
    {
        public override void UpdateVisualization(AbilityPlacement abilityPlacement)
        {
            if (abilityPlacement is ClickAbilityPlacement clickPlacement)
            {
                transform.position = clickPlacement.Position;
            }
        }
    }
}
