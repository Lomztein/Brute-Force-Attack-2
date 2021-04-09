using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lomztein.BFA2.Plugins
{
    public static class PluginLoader
    {
        public static Assembly LoadAssembly(string path)
        {
            return Assembly.LoadFrom(path);
        }

        public static IEnumerable<Type> FindPluginTypes (Assembly assembly)
        {
            foreach (Type type in assembly.GetExportedTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<IPlugin> InstantiatePlugins (Assembly assembly)
        {
            foreach (Type type in FindPluginTypes(assembly))
            {
                yield return (IPlugin)Activator.CreateInstance(type);
            }
        }

        public static IEnumerable<IPlugin> InstantiatePlugins(string path) => InstantiatePlugins(LoadAssembly(path));
    }
}
