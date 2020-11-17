using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References.PrefabProviders
{
    public class ContentGameObjectPrefabProvider : MonoBehaviour, ICachedPrefabProvider
    {
        public string Path;

        public IContentCachedPrefab[] Get()
            => Content.GetAll(Path, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
    }
}
