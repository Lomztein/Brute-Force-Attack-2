using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Abilities.Visualizers
{
    public class CompositeAbilityVisualizer : AbilityVisualizer
    {
        [ModelReference]
        public AbilityVisualizer[] Visualizers;

        public override void UpdateVisualization(AbilityPlacement abilityPlacement)
        {
            foreach (var visualizer in Visualizers)
            {
                visualizer.UpdateVisualization(abilityPlacement);
            }
        }
    }
}
