using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Plugins
{
    internal class PluginManager
    {
        private List<IPlugin> _loadedPlugins = new List<IPlugin>();
        private Facade _facade;

        public int LoadedCount => _loadedPlugins.Count;

        internal void LoadPlugin (string path)
        {
            Assembly assembly = PluginLoader.LoadAssembly(path);
            _loadedPlugins.AddRange(PluginLoader.InstantiatePlugins(assembly));
            ReflectionUtils.GameAssemblies.Add(assembly);
        }

        internal void LoadAllPlugins(string[] paths)
        {
            foreach (string path in paths)
            {
                LoadPlugin(path);
            }
        }

        internal void StartPlugins ()
        {
            foreach (IPlugin plugin in _loadedPlugins)
            {
                plugin.Start(Facade.GetInstance());
            }
        }

        internal void StopPlugins ()
        {
            foreach (IPlugin plugin in _loadedPlugins)
            {
                plugin.Stop(_facade);
            }
        }
    }
}
