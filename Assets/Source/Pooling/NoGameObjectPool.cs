using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Pooling
{
    public class NoGameObjectPool<T> : IObjectPool<T> where T : IPoolObject
    {
        public event Action<T> OnNew;

        private GameObject _prefab;

        public NoGameObjectPool (GameObject prefab)
        {
            _prefab = prefab;
        }

        public void Clear()
        {
        }

        public T Get()
        {
            GameObject obj = UnityEngine.Object.Instantiate(_prefab);
            T component = obj.GetComponent<T>();
            OnNew?.Invoke(component);
            return component;
        }

        public void Insert(T obj)
        {
            obj.DestroySelf();
        }
    }
}
