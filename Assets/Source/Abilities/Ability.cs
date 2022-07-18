using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Abilities.Visualizers;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    public abstract class Ability : ScriptableObject, INamed, IIdentifiable
    {
        [SerializeField, ModelProperty] private string _name;
        [SerializeField, ModelProperty] private string _description;
        [SerializeField, ModelProperty] private string _identifier;

        public string Name => _name;
        public string Description => _description;
        public string Identifier => _identifier;

        [ModelProperty] public ContentSpriteReference Sprite;
        [ModelProperty] public int MaxCharges = 1;
        [ModelProperty] public int MaxCooldown;
        [ModelProperty] public IResourceCost ActivationCost;
        [ModelProperty] public ContentPrefabReference Visualizer;

        public int CurrentCooldown;
        public int CurrentCharges;

        public virtual void Select ()
        {
            var placement = GetPlacement();
            placement.Assign(this, InstantiateVisualizer());
            if (placement != null)
            {
                PlacementController.Instance.TakePlacement(placement);
            }
            else
            {
                Activate(null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Ability placement, by default a ClickAbilityPlacement</returns>
        public virtual AbilityPlacement GetPlacement()
        {
            return new ClickAbilityPlacement();
        }

        public virtual AbilityVisualizer InstantiateVisualizer()
        {
            return Visualizer.Instantiate().GetComponent<AbilityVisualizer>();
        }

        public virtual void Activate(AbilityPlacement placement)
        {
            if (CurrentCooldown != 0 || CurrentCharges == MaxCharges)
            {
                CurrentCooldown = MaxCooldown;
            }
            CurrentCharges--;
        }

        public virtual void Cooldown(int amount)
        {
            CurrentCooldown = Mathf.Max(0, CurrentCooldown - amount);
            if (CurrentCooldown == 0 && CurrentCharges < MaxCharges)
            {
                CurrentCooldown = MaxCooldown;
                CurrentCharges++;
            }
            if (CurrentCharges == MaxCharges)
            {
                CurrentCooldown = 0;
            }
        }
    }
}
