using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Plugins;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.UI.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentManager : MonoBehaviour
    {
        public static ContentManager Instance;
        readonly IContentPackSource _source = new ContentPackSource();

        private const string WILDCARD = "*";
        private List<IContentPack> _activePacks;
        private PluginManager _pluginManager = new PluginManager();

        private Dictionary<string, object> _cache = new Dictionary<string, object>();

        public IEnumerable<IContentPack> GetContentPacks()
        {
            if (_activePacks == null)
            {
                _activePacks = new List<IContentPack>();
                IEnumerable<IContentPack> loaded = _source.GetPacks();
                Message.Send("Loaded " + loaded.Count() + " content packs.", Message.Type.Minor);
                _activePacks.AddRange(loaded);
            }
            return _activePacks;
        }

        internal void LoadPlugins ()
        {
            foreach (IContentPack pack in GetContentPacks())
            {
                if (pack is ContentPack contentPack)
                {
                    _pluginManager.LoadAllPlugins(contentPack.GetPluginAssemblies());
                }
            }

            if (_pluginManager.LoadedCount > 0)
            {
                Message.Send("Loaded " + _pluginManager.LoadedCount + " plugin assemblies.", Message.Type.Minor);
            }

            _pluginManager.StartPlugins();
        }

        internal void InitializeContent ()
        {
            GetContentPacks();
            Instance = this;
        }

        private string OSAgnosticPath(string path)
        {
            return
                path.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

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
            else
            {
                _cache.Add(path, obj);
            }
            return obj;
        }

        public object GetContent(string path, Type type, bool useCache)
        {
            path = OSAgnosticPath(path);

            if (useCache)
            {
                object cache = GetCache(path);
                if (!IsCacheValid(cache))
                {
                    return SetCache(path, GetPack(GetPackFolder(path)).GetContent(GetContentPath(path), type));
                }
                else
                {
                    return cache;
                }
            }
            else
            {
                return GetPack(GetPackFolder(path)).GetContent(GetContentPath(path), type);
            }
        }

        private bool IsCacheValid (object cacheObj)
        {
            if (cacheObj == null)
            {
                return false;
            }
            if (cacheObj is IDisposableContent disposable)
            {
                return !disposable.IsDisposed();
            }
            if (cacheObj is object[] array)
            {
                return array.All(x => IsCacheValid(x));
            }
            return true;
        }

        public void ClearCache (string path)
        {
            object cache = GetCache(path);
            DisposeCacheObject(cache);
            _cache.Remove(path);
        }

        private void DisposeCacheObject (object cacheObj)
        {
            if (cacheObj is object[] array)
            {
                foreach (object obj in array)
                {
                    DisposeCacheObject(obj);
                }
            } else if (cacheObj is IDisposableContent disposable)
            {
                disposable.Dispose();
            }
            
            if (IsCacheValid(cacheObj))
            {
                throw new Exception("An object cache was disposed but is still considered valid. Plz fix.");
            }
        }

        private string GetPackFolder(string path)
        {
            return path.Split(Path.DirectorySeparatorChar).First();
        }

        private string GetContentPath(string path)
        {
            return path.Substring(path.IndexOf(Path.DirectorySeparatorChar) +1);
        }

        public object[] GetAllContent(string path, Type type, bool useCache)
        {
            path = OSAgnosticPath(path);

            if (useCache)
            {
                object cache = GetCache(path);
                if (IsCacheValid(cache))
                {
                    return cache as object[];
                }
            }

            string packFolder = GetPackFolder(path);
            bool wildcard = packFolder == WILDCARD;

            string contentPath = GetContentPath(path);

            if (wildcard)
            {
                List<object> objects = new List<object>();
                foreach (IContentPack pack in GetContentPacks())
                {
                    objects.AddRange(pack.GetAllContent(contentPath, type));
                }

                object[] array = objects.ToArray();
                if (useCache)
                {
                    SetCache(path, array);
                }
                return array;
            }
            else
            {
                object[] array = GetPack(packFolder).GetAllContent(contentPath, type);
                if (useCache)
                {
                    SetCache(path, array);
                }
                return array;
            }
        }

        private IContentPack GetPack(string name)
        {
            IContentPack pack = GetContentPacks().FirstOrDefault(x => x.Name == name);
            if (pack == null)
            {
                throw new ArgumentException($"Content Pack '{name}' not found.");
            }
            return pack;
        }
    }
}
