using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.ContentSystem.Objects;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References.PrefabProviders
{
    public class CachedPrefabProvider : MonoBehaviour, ICachedPrefabProvider
    {
        public IContentCachedPrefab[] Prefabs { get; set; }

        public IContentCachedPrefab[] Get() => Prefabs;
    }
}
