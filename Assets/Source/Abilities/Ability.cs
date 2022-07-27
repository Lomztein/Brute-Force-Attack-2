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
        [SerializeField, ModelProperty] protected string _name;
        [SerializeField, ModelProperty] protected string _description;
        [SerializeField, ModelProperty] protected string _identifier;

        public string Name => _name;
        public string Description => _description;
        public string Identifier => _identifier;

        [ModelProperty] public ContentSpriteReference Sprite;
        [ModelProperty] public int MaxCharges = 1;
        [ModelProperty] public int MaxCooldown;
        [ModelProperty] public ResourceCost ActivationCost;
        [ModelProperty] public ContentPrefabReference Visualizer;

        [ModelProperty] public int CurrentCooldown;
        [ModelProperty] public int CurrentCharges;

        public virtual void Select ()
        {
            var placement = GetPlacement();
            if (placement != null)
            {
                placement.Assign(this, InstantiateVisualizer());
                PlacementController.Instance.TakePlacement(placement);
            }
            else if (Player.Player.Resources.TrySpend(ActivationCost))
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
            if (Visualizer != null)
            {
                return Visualizer.Instantiate().GetComponent<AbilityVisualizer>();
            }
            return null;
        }

        public virtual void Activate(AbilityPlacement placement)
        {
            if (CurrentCooldown != 0 || CurrentCharges == MaxCharges)
            {
                CurrentCooldown = MaxCooldown;
            }
            if (MaxCooldown != 0)
            {
                CurrentCharges--;
            }
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

        public virtual bool Available (out IEnumerable<string> reasons)
        {
            List<string> reasonsList = new List<string>();
            if (!Player.Player.Resources.HasEnough(ActivationCost))
            {
                reasonsList.Add("Not enough resources");
            }
            if (CurrentCharges == 0)
            {
                reasonsList.Add("Cooldown: " + CurrentCooldown);
            }
            reasons = reasonsList;
            return reasonsList.Count == 0;
        }
    }
}
