﻿using System;
using UnityEngine;

namespace Lomztein.BFA2.Content.Objects
{
    public class SceneCachedGameObject : IContentCachedPrefab, IDisposable
    {
        private const string CACHE_OBJ_NAME = "_GO_CACHE";
        private static GameObject _cacheObj;

        private GameObject _cache;

        public SceneCachedGameObject(GameObject cache)
        {
            _cache = cache;
            CacheCache();
        }

        private void CacheCache ()
        {
            _cache.SetActive(false);
            _cache.transform.SetParent(GetCacheObject().transform);
        }

        private static GameObject GetCacheObject ()
        {
            if (_cacheObj == null)
            {
                _cacheObj = new GameObject(CACHE_OBJ_NAME);
            }
            return _cacheObj;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_cache);
        }

        public override string ToString()
        {
            return GetCache().ToString();
        }

        public static void Clear ()
        {
            foreach (Transform child in GetCacheObject().transform)
            {
                UnityEngine.Object.Destroy(child);
            }
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
    }
}
