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
        [ModelAssetReference]
        public Item Item;

        protected override void Start()
        {
            Sprite = Item.Sprite;
            SpriteTint = Item.SpriteTint;
            base.Start();
        }

        protected override void Collect()
        {
            Player.Player.Inventory.AddItem(Instantiate(Item));
        }
    }
}
