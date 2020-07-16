using System;
using Lomztein.BFA2.Serialization.Assemblers;
using Lomztein.BFA2.Serialization.Assemblers.Turret;
using Lomztein.BFA2.Serialization.Models.GameObject;
using Lomztein.BFA2.Serialization.Models.Turret;
using Lomztein.BFA2.Turrets;
using UnityEngine;

namespace Lomztein.BFA2.Content.Objects
{
    public class ContentCachedTurretAssemblyPrefab : IContentCachedPrefab
    {
        private SceneCachedGameObject _cache;

        public ContentCachedTurretAssemblyPrefab(ITurretAssemblyModel model)
        {
            GameObjectTurretAssemblyAssembler assembler = new GameObjectTurretAssemblyAssembler();
            GameObject instance = (assembler.Assemble(model) as Component).gameObject;

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
