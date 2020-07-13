using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Content.Objects;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.PrefabProviders
{
    public class CompositeCachedPrefabProvider : MonoBehaviour, ICachedPrefabProvider
    {
        public IContentCachedPrefab[] Get()
            => GetComponentsInChildren<ICachedPrefabProvider>().Where(x => !x.Equals(this)).SelectMany(x => x.Get()).ToArray();
    }
}
