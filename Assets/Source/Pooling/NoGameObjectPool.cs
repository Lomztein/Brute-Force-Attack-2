using Lomztein.BFA2.ContentSystem.Objects;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Pooling
{
    public class NoGameObjectPool<T> : IObjectPool<T> where T : IPoolObject
    {
        public event Action<T> OnNew;

        private IContentPrefab _prefab;

        public NoGameObjectPool (IContentPrefab prefab)
        {
            _prefab = prefab;
        }

        public void Clear()
        {
        }

        public T Get()
        {
            GameObject obj = _prefab.Instantiate();
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
