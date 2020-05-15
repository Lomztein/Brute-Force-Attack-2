using Lomztein.BFA2.Content.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Pooling
{
    public class GameObjectPool<T> : IObjectPool<T> where T : IPoolObject
    {
        public event Action<T> OnNew;
        private List<T> _objects = new List<T>();
        private IContentGameObject _prefab;

        public GameObjectPool (IContentGameObject prefab)
        {
            _prefab = prefab;
        }

        public void Clear()
        {
            foreach (T obj in _objects)
            {
                obj.DestroySelf ();
            }
            _objects.Clear();
        }

        public T Get()
        {
            T obj = _objects.FirstOrDefault(x => x.Ready == true);
            if (obj == null)
            {
                obj = _prefab.Instantiate().GetComponent<T>();
                _objects.Add(obj);
                OnNew?.Invoke(obj);
            }
            obj.EnableSelf();
            return obj;
        }

        public void Insert(T obj)
        {
            obj.DisableSelf();
        }
    }
}
