using Lomztein.BFA2.Abilities.Effects;
using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.Abilities.Visualizers;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Abilities
{
    public abstract class Ability : ScriptableObject, INamed, IIdentifiable
    {
        [SerializeField, ModelProperty] protected string _name;
        [SerializeField, ModelProperty] protected string _description;
        [SerializeField, ModelProperty] protected string _identifier;

        public abstract float CooldownProgress { get; }
        public abstract int Charges { get; }

        public string Name => _name;
        public string Description => _description;
        public string Identifier => _identifier;

        [ModelProperty] public ContentSpriteReference Sprite;
        [ModelProperty] public ResourceCost ActivationCost;
        [ModelProperty, SerializeReference, SR]
        public IAbilityEffect Effect;
        [ModelProperty, SerializeReference, SR]
        public AbilityPlacement Placement;
        [ModelProperty]
        public ContentPrefabReference Visualizer;

        public event Action<Ability, AbilityPlacement> OnActivated;

        public virtual void Select()
        {
            var placement = GetPlacement();
            if (placement != null)
            {
                placement.Assign(this, InstantiateVisualizer());
                PlacementController.Instance.TakePlacement(placement);
            }
            else if (Player.Player.Resources.TrySpend(ActivationCost))
            {
                GetEffect().Activate(null);
            }
        }

        public virtual IAbilityEffect GetEffect()
            => Effect;

        public virtual AbilityPlacement GetPlacement()
            => Placement;

        public virtual AbilityVisualizer InstantiateVisualizer()
            => Visualizer.Instantiate().GetComponent<AbilityVisualizer>();

        public bool IsAvailable() => !GetUnavailableReasons().Any();

        public virtual IEnumerable<string> GetUnavailableReasons()
        {
            if (!Player.Player.Resources.HasEnough(ActivationCost))
            {
                yield return "Not enough resources.";
            }
        }

        public virtual void Activate()
        {
            GetEffect().Activate(GetPlacement());
            OnActivated?.Invoke(this, GetPlacement());
        }
    }
}
