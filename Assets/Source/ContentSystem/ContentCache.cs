using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentCache
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        internal static bool IsCacheValid(object cacheObj)
        {
            if (cacheObj == null)
            {
                return false;
            }
            if (cacheObj is IDisposableContent disposable)
            {
                return !disposable.IsDisposed();
            }
            return true;
        }

        internal object GetCache(string key)
        {
            if (_cache.TryGetValue(key, out object cache) && !IsCacheValid(cache))
            {
                throw new InvalidOperationException($"Cache '{key}' is unset or invalid.");
            }
            return cache;
        }

        internal bool TryGetCache(string key, out object cache)
        {
            if (_cache.TryGetValue(key, out cache) && IsCacheValid(cache))
            {
                return true;
            }
            return false;
        }

        internal void SetCache(string key, object obj)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, obj);
            }
            else
            {
                _cache[key] = obj;
            }
            if (obj is UnityEngine.Object uObj)
            {
                ContentCacheUnityObjectTracker.AddObject(uObj);
            }
        }

        internal void ClearCache()
        {
            var toRemove = _cache.ToArray();
            foreach (var pair in toRemove)
            {
                ClearCache(pair.Key);
            }
            _cache.Clear();
            ContentCacheUnityObjectTracker.ClearCache();
        }

        internal void ClearCache(string key)
        {
            if (_cache.ContainsKey(key))
            {
                object cache = GetCache(key);
                DisposeCacheObject(cache);
                _cache.Remove(key);
            }
        }

        private void DisposeCacheObject(object cacheObj)
        {
            bool checkValidity = false;

            if (cacheObj is IDisposableContent disposable)
            {
                disposable.Dispose();
                checkValidity = true;
            }

            if (checkValidity && IsCacheValid(cacheObj))
            {
                throw new Exception($"An object cache of type {cacheObj.GetType()} was disposed but is still considered valid. Plz fix.");
            }
        }
    }
}
