using Lomztein.BFA2.Modification.ModProviders.ExpansionCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Inventory.Items
{
    public class ExpansionCardItem : IdentifiableCachedPrefabItem
    {
        protected override void Init()
        {
            var prefab = GetPrefab();
            IExpansionCard card = prefab.GetComponent<IExpansionCard>();
            Name = card.Name;
            Description = card.Description;
            Sprite = card.Sprite;
        }
    }
}
