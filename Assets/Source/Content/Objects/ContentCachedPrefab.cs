using System;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Models.GameObject;
using UnityEngine;

namespace Lomztein.BFA2.Content.Objects
{
    [Serializable]
    public class ContentCachedPrefab : IContentCachedPrefab
    {
        private SceneCachedGameObject _cache;

        // TODO: Find a way to unify different cached prefab type classes.
        public ContentCachedPrefab(IGameObjectModel model)
        {
            IGameObjectAssembler assembler = new GameObjectAssembler();
            GameObject instance = assembler.Assemble(model);

            _cache = new SceneCachedGameObject(instance);
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
