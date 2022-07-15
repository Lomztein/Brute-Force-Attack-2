using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Plugins;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.UI.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentManager : MonoBehaviour
    {
        public static ContentManager Instance;
        readonly IContentPackSource _source = new ContentPackSource();

        private List<IContentPack> _activePacks;
        private PluginManager _pluginManager = new PluginManager();

        private ContentIndex _index = new ContentIndex();
        private ContentCache _cache = new ContentCache();

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
            foreach (var pack in _activePacks)
            {
                _index.AddIndices(pack.GetContentPaths().Select(y => pack.Name + Path.DirectorySeparatorChar + y));
            } 
            Instance = this;
        }

        private string OSAgnosticPath(string path)
        {
            return
                path.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);
        }

        public object GetCacheOrLoadContent(string path, Type type)
        {
            path = OSAgnosticPath(path);
            if (_cache.TryGetCache(path, out object cache))
            {
                return cache;
            }
            return LoadContent(path, type);
        }

        public object LoadContent(string path, Type type)
        {
            path = OSAgnosticPath(path);
            object obj = GetPack(GetPackFolder(path)).LoadContent(GetContentPath(path), type);
            _cache.SetCache(path, obj);
            return obj;
        }

        private string GetPackFolder(string path)
        {
            return path.Split(Path.DirectorySeparatorChar).First();
        }

        private string GetContentPath(string path)
        {
            return path.Substring(path.IndexOf(Path.DirectorySeparatorChar) + 1);
        }

        public IEnumerable<object> GetCacheOrLoadAllContent(string path, Type type)
        {
            path = OSAgnosticPath(path);
            var query = _index.Query(path);
            foreach (var match in query)
            {
                if (_cache.TryGetCache(match, out object cache))
                {
                    yield return cache;
                }
                else
                {
                    yield return LoadContent(match, type);
                }
            }
        }

        public IEnumerable<object> LoadAllContent(string path, Type type)
        {
            path = OSAgnosticPath(path);
            var query = _index.Query(path);
            foreach (var match in query)
            {
                yield return LoadContent(match, type);
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

        public void ClearCache() => _cache.ClearCache();
        public void ClearCache(string path) => _cache.ClearCache(path);
        public IEnumerable<string> QueryContentIndex(string pattern) => _index.Query(pattern);
    }
}
