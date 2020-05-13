using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    public class GameObjectPrefab : IDisposable
    {
        private const string PREFAB_PARENT_NAME = "_PREFABS";
        private static GameObject _prefabParent;

        private GameObject _prefab;

        private GameObject GetParent ()
        {
            if (_prefabParent == null)
            {
                _prefabParent = new GameObject(PREFAB_PARENT_NAME);
            }
            return _prefabParent;
        }

        public GameObjectPrefab (GameObject prefab)
        {
            _prefab = UnityEngine.Object.Instantiate(prefab, GetParent().transform);
            _prefab.SetActive(false);
        }

        public GameObject Instantiate ()
        {
            _prefab.SetActive(true);
            GameObject obj = UnityEngine.Object.Instantiate(_prefab);
            _prefab.SetActive(false);
            return obj;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_prefab);
        }
    }
}
