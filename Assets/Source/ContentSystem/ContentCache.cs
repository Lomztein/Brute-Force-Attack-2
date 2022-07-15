using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class ContentCache
    {
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        public static bool IsCacheValid(object cacheObj)
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

        public object GetCache(string key)
        {
            if (_cache.TryGetValue(key, out object cache) && !IsCacheValid(cache))
            {
                throw new InvalidOperationException("Cache is unset or invalid.");
            }
            return cache;
        }

        public bool TryGetCache(string key, out object cache)
        {
            if (_cache.TryGetValue(key, out cache) && IsCacheValid(cache))
            {
                return true;
            }
            return false;
        }

        public void SetCache(string key, object obj)
        {
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, obj);
            }
            else
            {
                _cache[key] = obj;
            }
        }

        public void ClearCache()
        {
            var toRemove = _cache.ToArray();
            foreach (var pair in toRemove)
            {
                ClearCache(pair.Key);
            }
        }

        public void ClearCache(string key)
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
            if (cacheObj is UnityEngine.Object uobj)
            {
                UnityEngine.Object.Destroy(uobj);
            }
            if (cacheObj is IDisposableContent disposable)
            {
                disposable.Dispose();
            }

            if (IsCacheValid(cacheObj))
            {
                throw new Exception("An object cache was disposed but is still considered valid. Plz fix.");
            }
        }
    }
}
