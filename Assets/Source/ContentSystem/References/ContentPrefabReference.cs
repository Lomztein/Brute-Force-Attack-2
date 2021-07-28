using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.References
{
    [Serializable]
    public class ContentPrefabReference : IContentCachedPrefab
    {
        [ModelProperty]
        public string Path;

        private IContentCachedPrefab _cachedPrefab;

        public ContentPrefabReference () { }

        public ContentPrefabReference (string path)
        {
            Path = path;
        }

        private IContentCachedPrefab Get ()
        {
            if (_cachedPrefab == null || _cachedPrefab.IsDisposed())
            {
                _cachedPrefab = Content.Get(Path, typeof(IContentCachedPrefab)) as IContentCachedPrefab;
            }
            return _cachedPrefab;
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

        public bool IsDisposed()
        {
            return _cachedPrefab.IsDisposed();
        }
    }
}
