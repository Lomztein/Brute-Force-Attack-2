using Lomztein.BFA2.Content.References.GameObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References
{
    [Serializable]
    public class ContentGameObject : IContentGameObject, IDisposable, ISerializable
    {
        public string Path;
        private GameObject _cache;

        public ContentGameObject () { }

        public ContentGameObject (string path)
        {
            Path = path;
        }

        public ContentGameObject(GameObject cache)
        {
            _cache = cache;
        }

        private GameObject GetCache ()
        {
            if (_cache == null)
            {
                _cache = Content.Get(Path, typeof(GameObject)) as GameObject;
                _cache.SetActive(false);
            }
            return _cache;
        }

        public GameObject Instantiate()
        {
            GameObject go = UnityEngine.Object.Instantiate(GetCache());
            go.SetActive(true);
            Debug.Log(go, go);
            return go;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_cache);
        }

        public JToken Serialize()
        {
            return new JValue(Path);
        }

        public void Deserialize(JToken source)
        {
            Path = source.ToString();
        }
    }
}
