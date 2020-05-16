using Lomztein.BFA2.Serialization.Assemblers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.GameObjects
{
    public class CachedGameObject : IContentGameObject, IDisposable
    {
        private const string CACHE_OBJ_NAME = "_GO_CACHE";
        private static GameObject _cacheObj;

        private IContentGameObject _contentGO;


        private GameObject _cache;

        public CachedGameObject (IContentGameObject contentGO)
        {
            _contentGO = contentGO;
        }

        public CachedGameObject(GameObject cache)
        {
            _cache = cache;
            CacheCache();
        }

        private void CacheCache ()
        {
            _cache.SetActive(false);
            _cache.transform.SetParent(GetCacheObject().transform);
        }

        private GameObject GetCacheObject ()
        {
            if (_cacheObj == null)
            {
                _cacheObj = new GameObject(CACHE_OBJ_NAME);
            }
            return _cacheObj;
        }

        public GameObject Get ()
        {
            if (_cache == null)
            {
                _cache = _contentGO.Instantiate();
                CacheCache();
            }
            return _cache;
        }

        public GameObject Instantiate ()
        {
            GameObject obj = UnityEngine.Object.Instantiate(Get());
            obj.SetActive(true);
            return obj;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_cache);
        }

        public override string ToString()
        {
            return Get().ToString();
        }
    }
}
