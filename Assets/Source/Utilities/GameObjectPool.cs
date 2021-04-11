using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class GameObjectPool : MonoBehaviour
    {
        private static GameObjectPool instance;
        public static GameObjectPool Instance {
            get {
                if (instance != null) return instance;
                var go = new GameObject();
                instance = go.AddComponent<GameObjectPool>();
                return instance;
            }
        }

        private Dictionary<Object, ObjectPool<Object>> poolDict = new Dictionary<Object, ObjectPool<Object>>();

        public void Return<T>(T type, T instance) where T : Object
        {
            if (!poolDict.TryGetValue(type, out var pool)) {
                pool = new ObjectPool<Object>(() => Instantiate(type));
                poolDict[type] = pool;
            }
            pool.Return(instance);
        }
    
        public T Get<T>(T type) where T : Object
        {
            if (!poolDict.TryGetValue(type, out var pool)) {
                pool = new ObjectPool<Object>(() => Instantiate(type));
                poolDict[type] = pool;
            }
            return (T)pool.Get();
        }
    
        public T Get<T>(T type, Vector3 position, Quaternion rotation) where T : Behaviour
        {
            var obj = Get(type);
            var objTransform = obj.transform;
            objTransform.position = position;
            objTransform.rotation = rotation;
            return obj;
        }
    
        public GameObject Get(GameObject type, Vector3 position, Quaternion rotation)
        {
            var obj = Get(type);
            var objTransform = obj.transform;
            objTransform.position = position;
            objTransform.rotation = rotation;
            return obj;
        }
    }
}