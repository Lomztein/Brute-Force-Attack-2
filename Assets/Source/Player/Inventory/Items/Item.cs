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
    public abstract class Item
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Sprite Sprite { get; }
        public abstract Color SpriteTint { get; }
    }
}
