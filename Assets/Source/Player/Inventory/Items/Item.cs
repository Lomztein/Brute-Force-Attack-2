using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Inventory.Items
{
    [Serializable]
    public abstract class Item : ScriptableObject
    {
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public Colorization.Color Color;
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public Color SpriteTint;

        public virtual void OnAddedToInventory(IInventory inventory) { }
        public virtual void OnRemovedFromInventory(IInventory inventory) { }
    }
}
