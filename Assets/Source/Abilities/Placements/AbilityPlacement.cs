using Lomztein.BFA2.Abilities.Visualizers;
using Lomztein.BFA2.Placement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities.Placements
{
    public abstract class AbilityPlacement : IPlacement
    {
        public event Action OnFinished;

        public Ability Ability { get; private set; }
        public AbilityVisualizer Visualizer { get; private set; }

        public virtual void Assign(Ability ability, AbilityVisualizer visualizer)
        {
            Ability = ability;
            Visualizer = visualizer;
        }

        public virtual bool Finish()
        {
            UnityEngine.Object.Destroy(Visualizer.gameObject);
            OnFinished?.Invoke();
            return true;
        }

        public virtual void Init()
        {
        }

        protected void UpdateVisualizer ()
        {
            if (Visualizer != null) Visualizer.UpdateVisualization(this);
        }
    }
}
