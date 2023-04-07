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
        private readonly Dictionary<object, string> _reverseCache = new Dictionary<object, string>();
        // I wonder if some sort of double-hashmap that functions like this already exists :thinking:
        // Can't find anything about it, so now I'm wondering if there is a good reason it doesn't.
        // Surely it can't be *that* much of a war crime to do :hehehe:
        //
        // Nvm I looked again and found that someone had made it. I stole it! :D
        // TODO: Change this to use the Map class instead of double dicts.

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

        internal string GetKey(object cachedObj)
        {
            if (!IsCacheValid(cachedObj))
            {
                Debug.LogWarning("The given object has been disposed, key may be incorrect.");
            }
            if (_reverseCache.TryGetValue(cachedObj, out string key))
            {
                return key;
            }
            throw new KeyNotFoundException("No key has been cached for the given object.");
        }

        internal bool TryGetCache(string key, out object cache)
        {
            if (_cache.TryGetValue(key, out cache) && IsCacheValid(cache))
            {
                return true;
            }
            return false;
        }

        internal bool TryGetKey(object cachedObj, out string key)
            => _reverseCache.TryGetValue(cachedObj, out key);

        internal void SetCache(string key, object obj)
        {
            if (!_cache.ContainsKey(key))
                _cache.Add(key, obj);
            else
                _cache[key] = obj;


            if (!_reverseCache.ContainsKey(obj))
                _reverseCache.Add(obj, key);
            else
                _reverseCache[obj] = key;

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
            _reverseCache.Clear();
            ContentCacheUnityObjectTracker.ClearCache();
        }

        internal void ClearCache(string key)
        {
            if (_cache.ContainsKey(key))
            {
                object cache = GetCache(key);
                DisposeCacheObject(cache);
                _cache.Remove(key);
                _reverseCache.Remove(cache);
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
