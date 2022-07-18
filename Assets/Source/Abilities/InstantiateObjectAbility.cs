using Lomztein.BFA2.Abilities.Placements;
using Lomztein.BFA2.ContentSystem.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    [CreateAssetMenu(fileName = "New " + nameof(InstantiateObjectAbility), menuName = "BFA2/Abilities/Instantiate Object Ability")]
    public class InstantiateObjectAbility : Ability
    {
        public ContentPrefabReference ToInstantiate;

        public override void Activate(AbilityPlacement placement)
        {
            if (placement is ClickAbilityPlacement clickPlacement) {
                base.Activate(placement);
                GameObject newGo = ToInstantiate.Instantiate();
                newGo.transform.position = clickPlacement.CurrentPosition;
            }
        }
    }
}
