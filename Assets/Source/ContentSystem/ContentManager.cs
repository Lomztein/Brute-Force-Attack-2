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
        public static bool NeedsContentReload { get; private set; }
        public static bool NeedsApplicationReload => PluginManager.PluginsCleared;
        public static int LoadedContentPackCount => Instance._loadedAndActivePacks.Count;

        readonly IContentPackSource _source = new ContentPackSource();
        private readonly List<IContentPack> _loadedAndActivePacks = new List<IContentPack>();

        private ContentIndex _index = new ContentIndex();
        private ContentCache _cache = new ContentCache();

        public static event Action<IEnumerable<IContentPack>> OnPreContentReload;
        public static event Action<IEnumerable<IContentPack>> OnPostContentReload;

        private void Awake()
        {
            Instance = this;
        }

        internal static void SaveEnabledContentPackIdentifiers(IEnumerable<string> enabledPlugins)
        {
            PlayerPrefs.SetString("EnabledContentPacks", string.Join("\n", enabledPlugins));
        }

        public static IEnumerable<string> GetEnabledContentPackIdentifiers ()
        {
            return PlayerPrefs.GetString("EnabledContentPacks", string.Join("\n", GetDefaultContentPacks())).Split('\n');
        }

        private static IEnumerable<string> GetDefaultContentPacks ()
        {
            yield return "Brute Force Attack 2-Core";
            yield return "Brute Force Attack 2-Resources";
            yield return "You-Custom";
        }

        internal void ReloadContent ()
        {
            OnPreContentReload?.Invoke(_loadedAndActivePacks);

            Debug.Log("Stopping plugins..");
            PluginManager.StopPlugins();
            PluginManager.ClearPlugins();

            Debug.Log("Clearing cache..");
            _cache.ClearCache();

            Debug.Log("Loading enabled content packs..");
            // Only load enabled packs.
            _loadedAndActivePacks.Clear();
            var allPacks = FindContentPacks();
            var shouldLoad = allPacks.Where(x => GetEnabledContentPackIdentifiers().Any(y => ContentPackUtils.MatchesUniqueIdentifier(x.GetUniqueIdentifier(), y, false)));
            _loadedAndActivePacks.AddRange(shouldLoad);

            Debug.Log("Clearing and setting up index..");
            ResetIndex();
            
            Debug.Log("Loading assemblies into memory..");
            LoadPluginAssembliesIntoMemory(_loadedAndActivePacks);

            Debug.Log("Starting plugins..");
            PluginManager.StartPlugins();

            NeedsContentReload = false;
            OnPostContentReload?.Invoke(_loadedAndActivePacks);
        }

        internal void EnableContentPack(string identifier)
        {
            if (!IsContentPackEnabled(identifier))
            {
                var enabled = GetEnabledContentPackIdentifiers().ToList();
                enabled.Add(identifier);
                SaveEnabledContentPackIdentifiers(enabled);
                NeedsContentReload = true;
            }
        }

        internal bool DisableContentPack(string identifier)
        {
            var enabled = GetEnabledContentPackIdentifiers().ToList();
            bool removed = enabled.Remove(identifier);
            SaveEnabledContentPackIdentifiers(enabled);
            NeedsContentReload = true;
            return removed;
        }

        internal bool SetContentPackEnabled(string identifier, bool enable)
        {
            if (enable)
            {
                EnableContentPack(identifier);
            }
            else
            {
                DisableContentPack(identifier);
            }
            return IsContentPackEnabled(identifier);
        }

        internal bool ToggleContentPackEnabled(string identifier)
            => SetContentPackEnabled(identifier, !IsContentPackEnabled(identifier));

        private void LoadPluginAssembliesIntoMemory(IEnumerable<IContentPack> from)
        {
            foreach (IContentPack pack in from)
            {
                if (pack is ContentPack contentPack)
                {
                    PluginManager.LoadPluginsIntoMemory(contentPack.GetPluginAssemblies());
                }
            }

            if (PluginManager.AnyLoadedPlugins)
            {
                Message.Send("Loaded " + PluginManager.LoadedPluginCount + " plugin assemblies.", Message.Type.Minor);
            }
        }

        public IEnumerable<IContentPack> FindContentPacks()
        {
            IEnumerable<IContentPack> loaded = _source.GetPacks();
            return loaded;
        }

        public  static bool IsContentPackEnabled(string identifier)
            => GetEnabledContentPackIdentifiers().Any(x => ContentPackUtils.MatchesUniqueIdentifier(x, identifier, false));

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
            IContentPack pack = _loadedAndActivePacks.FirstOrDefault(x => x.Name == name);
            if (pack == null)
            {
                throw new ArgumentException($"Content Pack '{name}' not found.");
            }
            return pack;
        }

        public void ClearCache() => _cache.ClearCache();
        public void ClearCache(string path) => _cache.ClearCache(path);
        public IEnumerable<string> QueryContentIndex(string pattern) => _index.Query(pattern);
        public void ResetIndex()
        {
            _index.ClearIndex();
            foreach (var pack in _loadedAndActivePacks)
            {
                _index.AddIndices(pack.GetContentPaths().Select(y => pack.Name + Path.DirectorySeparatorChar + y));
            }
        }
    }
}
