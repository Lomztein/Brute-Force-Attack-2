using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Plugins
{
    internal static class PluginManager
    {
        /// <summary>
        /// Returns true if the list of loaded plugins have been cleared at least once while plugins were loaded.
        /// It is adviced to restart the application if this returns true.
        /// </summary>
        public static bool PluginsCleared { get; private set; }

        private static readonly List<IPlugin> _loadedPlugins = new List<IPlugin>();

        public static bool AnyLoadedPlugins => _loadedPlugins.Any();
        public static IEnumerable<IPlugin> LoadedPlugins => _loadedPlugins;
        public static int LoadedPluginCount => _loadedPlugins.Count;

        internal static void LoadPluginIntoMemory (string path)
        {
            Assembly assembly = PluginLoader.LoadAssemblyIntoMemory(path);
            _loadedPlugins.AddRange(PluginLoader.InstantiatePlugins(assembly));
            ReflectionUtils.GameAssemblies.Add(assembly);
        }

        internal static void LoadPluginsIntoMemory(IEnumerable<string> paths)
        {
            foreach (string path in paths)
            {
                LoadPluginIntoMemory(path);
            }
        }

        internal static void ClearPlugins()
        {
            if (_loadedPlugins.Count > 0)
            {
                _loadedPlugins.Clear();
                PluginsCleared = true;
            }
        }

        internal static void StartPlugins ()
        {
            foreach (IPlugin plugin in _loadedPlugins)
            {
                plugin.Start();
            }
        }

        internal static void StopPlugins ()
        {
            foreach (IPlugin plugin in _loadedPlugins)
            {
                plugin.Stop();
            }
        }
    }
}
