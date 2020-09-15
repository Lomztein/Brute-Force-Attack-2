using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects
{
    public class MemoryCachedGameObject : IContentCachedPrefab
    {
        private GameObject _object;

        public MemoryCachedGameObject (GameObject obj)
        {
            _object = obj;
        }

        public void Dispose()
        {
            _object = null;
        }

        public GameObject GetCache()
        {
            return _object;
        }

        public GameObject Instantiate()
        {
            GameObject obj =  UnityEngine.Object.Instantiate(_object);
            obj.SetActive(true);
            return obj;
        }
    }
}
