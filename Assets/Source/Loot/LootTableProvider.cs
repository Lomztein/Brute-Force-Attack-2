using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Loot
{
    public class LootTableProvider : MonoBehaviour
    {
        public string Path;

        public ILootTable GetLootTable ()
        {
            IContentCachedPrefab[] prefabs = Content.Content.GetAll<IContentCachedPrefab>(Path);
            List<LootElement> elements = new List<LootElement>();

            foreach (var prefab in prefabs)
            {
                LootInfo info = prefab.GetCache().GetComponent<LootInfo>();
                elements.Add(new LootElement(info.Amount, info.Chance, prefab));
            }

            return new LootTable(elements.ToArray());
        }
    }
}
