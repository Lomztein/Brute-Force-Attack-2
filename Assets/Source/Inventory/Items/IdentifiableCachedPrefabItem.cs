using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.ExpansionCards;
using Lomztein.BFA2.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Inventory.Items
{
    public class IdentifiableCachedPrefabItem : Item
    {
        [ModelProperty]
        public ContentPrefabReference Prefab;

        public GameObject GetPrefab() => Prefab.GetCache();

        protected override void Init()
        {
            Sprite = Iconography.GenerateSprite(GetPrefab());
        }
    }
}
