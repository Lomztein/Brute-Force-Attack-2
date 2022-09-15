using Lomztein.BFA2.Abilities.Effects;
using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Assets.Source.Abilities.Effects
{
    public class InstantiateObjectEffect : IAbilityEffect
    {
        [ModelProperty]
        public ContentPrefabReference ToInstantiate;

        public void Activate(AbilityPlacement placement)
        {
            if (placement is ClickAbilityPlacement clickPlacement)
            {
                GameObject newGo = ToInstantiate.Instantiate();
                newGo.transform.position = clickPlacement.Position;
            }
        }
    }
}
