using System;
using Lomztein.BFA2.ContentSystem.Assemblers;
using Lomztein.BFA2.Serialization.Models;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects
{
    [Serializable]
    public class ContentCachedPrefab : IContentCachedPrefab
    {
        private SceneCachedGameObject _cache;

        public ContentCachedPrefab(RootModel model)
        {
            GameObjectAssembler assembler = new GameObjectAssembler();
            GameObject instance = assembler.Assemble(model);

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

        public bool IsDisposed()
        {
            return ((IDisposableContent)_cache).IsDisposed();
        }
    }
}
