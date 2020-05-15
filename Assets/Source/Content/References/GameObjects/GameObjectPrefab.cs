using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.GameObjects
{
    public class GameObjectPrefab : IDisposable
    {
        private const string PREFAB_PARENT_NAME = "_PREFABS";
        private static GameObject _prefabParent;

        public GameObject Prefab { get; private set; }

        private GameObject GetParent ()
        {
            if (_prefabParent == null)
            {
                _prefabParent = new GameObject(PREFAB_PARENT_NAME);
            }
            return _prefabParent;
        }

        public GameObjectPrefab(GameObject prefab, bool instantiateNewObject)
        {
            Prefab = instantiateNewObject ? UnityEngine.Object.Instantiate(prefab) : prefab;
            Prefab.transform.SetParent(GetParent().transform);
            Prefab.SetActive(false);
        }

        public GameObject Instantiate ()
        {
            Prefab.SetActive(true);
            GameObject obj = UnityEngine.Object.Instantiate(Prefab);
            obj.SetActive(true);
            Prefab.SetActive(false);
            return obj;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Prefab);
        }
    }
}
