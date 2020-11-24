using System;
using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects
{
    public class ContentCachedTurretAssemblyPrefab : IContentCachedPrefab
    {
        private SceneCachedGameObject _cache;

        public ContentCachedTurretAssemblyPrefab(ObjectModel model)
        {
            TurretAssemblyAssembler assembler = new TurretAssemblyAssembler();
            GameObject instance = (assembler.Assemble(model)).gameObject;

            _cache = new SceneCachedGameObject(instance);
        }

        public void Dispose()
        {
            ((IContentCachedPrefab)_cache).Dispose();
        }

        public GameObject GetCache()
        {
            return ((IContentCachedPrefab)_cache).GetCache();
        }

        public GameObject Instantiate()
        {
            return ((IContentCachedPrefab)_cache).Instantiate();
        }
    }
}
