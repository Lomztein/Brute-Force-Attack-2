using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentManager : MonoBehaviour, IContentManager
    {
        readonly IContentPackSource _source = new ContentPackSource();

        private const string WILDCARD = "*";
        private IContentPack[] _packs;

        private Dictionary<string, object> _cache = new Dictionary<string, object>();
        private bool _isInitialized = false;

        public IContentPack[] GetContentPacks() => _packs;

        private object GetCache (string path)
        {
            if (_cache.ContainsKey(path))
            {
                return _cache[path];
            }
            return null;
        }

        private object SetCache (string path, object obj)
        {
            if (_cache.ContainsKey(path))
            {
                _cache[path] = obj;
            }
            _cache.Add(path, obj);
            return obj;
        }

        private void TryInit ()
        {
            if (!_isInitialized)
            {
                Init();
            }
        }

        private void Init()
        {
            _packs = _source.GetPacks();
        }

        public object GetContent(string path, Type type)
        {
            TryInit();
            return GetCache(path) ?? SetCache (path, GetPack(GetPackFolder(path)).GetContent(GetContentPath(path), type));
        }

        private string GetPackFolder(string path)
        {
            return path.Split('/').First();
        }

        private string GetContentPath(string path)
        {
            return path.Substring(path.IndexOf('/')+1);
        }

        public object[] GetAllContent(string path, Type type)
        {
            TryInit();
            object cache = GetCache(path);
            if (cache != null)
            {
                return cache as object[];
            }

            string packFolder = GetPackFolder(path);
            bool wildcard = packFolder == WILDCARD;

            string contentPath = GetContentPath(path);

            if (wildcard)
            {
                List<object> objects = new List<object>();
                foreach (IContentPack pack in _packs)
                {
                    objects.AddRange(pack.GetAllContent(contentPath, type));
                }
                return SetCache (path, objects.ToArray()) as object[];
            }
            else
            {
                return SetCache(path, GetPack(packFolder).GetAllContent(contentPath, type)) as object[];
            }
        }

        private IContentPack GetPack(string name) => _packs.FirstOrDefault(x => x.Name == name);


    }
}
