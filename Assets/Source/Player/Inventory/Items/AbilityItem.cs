using Lomztein.BFA2.Abilities;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Inventory.Items
{
    public class AbilityItem : Item
    {
        [ModelProperty]
        public int ChargesPerItem;
        [ModelAssetReference]
        public Ability Ability;

        public override void OnAddedToInventory(IInventory inventory)
        {
            var existing = AbilityManager.Instance.GetAbility(Ability.Identifier);
            if (existing == null)
            {
                existing = Instantiate(Ability);
                AbilityManager.Instance.AddAbility(existing, this);
            }
        }

        public override void OnRemovedFromInventory(IInventory inventory)
        {
        }
    }
}
