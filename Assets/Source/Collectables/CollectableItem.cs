using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Inventory;
using Lomztein.BFA2.Inventory.Items;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Collectables
{
    public class CollectableItem : Collectable
    {
        [ModelProperty, SerializeReference, SR]
        public Item Item;

        protected override void Collect()
        {
            Player.Player.Inventory.AddItem(Item);
        }
    }
}
