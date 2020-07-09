using Lomztein.BFA2.Content.Objects;
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
    public class ContentPrefabReference : IContentCachedPrefab, ISerializable
    {
        public string Path;

        private IContentCachedPrefab _cachedPrefab;

        public ContentPrefabReference () { }

        public ContentPrefabReference (string path)
        {
            Path = path;
        }

        private IContentCachedPrefab Get ()
        {
            if (_cachedPrefab == null)
            {
                _cachedPrefab = Content.Get(Path, typeof(IContentCachedPrefab)) as IContentCachedPrefab;
            }
            return _cachedPrefab;
        }

        public JToken Serialize()
        {
            return new JValue(Path);
        }

        public void Deserialize(JToken source)
        {
            Path = source.ToString();
        }

        public GameObject GetCache()
        {
            return Get().GetCache();
        }

        public GameObject Instantiate()
        {
            return Get().Instantiate();
        }

        public void Dispose()
        {
            Get().Dispose();
        }
    }
}
