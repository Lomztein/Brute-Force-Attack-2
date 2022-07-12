using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.ContentSystem.Objects
{
    public class SceneCachedGameObject : IContentCachedPrefab, IDisposableContent
    {
        private readonly GameObject _cache;

        public SceneCachedGameObject(GameObject cache)
        {
            _cache = cache;
            CacheCache();
        }

        private void CacheCache ()
        {
            _cache.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(_cache);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_cache);
        }

        public override string ToString()
        {
            return GetCache().ToString();
        }

        public GameObject GetCache()
        {
            return _cache;
        }

        public GameObject Instantiate()
        {
            GameObject newObj = UnityEngine.Object.Instantiate(GetCache());
            newObj.SetActive(true);
            return newObj;
        }

        public bool IsDisposed()
            => _cache == null;
    }
}
