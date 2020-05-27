using Lomztein.BFA2.Content.Objects;
using Lomztein.BFA2.Serialization.Models.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.PrefabProviders
{
    public class ContentGameObjectPrefabProvider : MonoBehaviour, ICachedPrefabProvider
    {
        public string Path;

        public IContentCachedPrefab[] Get()
            => Content.GetAll(Path, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
    }
}
