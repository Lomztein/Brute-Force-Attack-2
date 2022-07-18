using Lomztein.BFA2.Abilities.Placements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.Abilities.Visualizers
{
    public abstract class AbilityVisualizer : MonoBehaviour
    {
        protected Ability Ability { get; private set; }

        public void Assign(Ability ability)
        {
            Ability = ability;
        }

        public abstract void UpdateVisualization(AbilityPlacement abilityPlacement);
    }
}
