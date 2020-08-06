using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Utilities
{
    public static class ReflectionUtils
    {
        public static List<Assembly> GameAssemblies = new List<Assembly>()
        {
            typeof(ReflectionUtils).Assembly,
        };

        public static void DynamicBroadcastInvoke(GameObject parent, string method)
        {
            Component[] components = parent.GetComponentsInChildren<Component>();
            foreach (Component component in components)
            {
                DynamicInvoke(component, method);
            }
        }

        public static object DynamicInvoke(object obj, string method)
            => DynamicInvoke(obj, method, Array.Empty<object>());

        public static object DynamicInvoke(object obj, string method, params object[] args)
        {
            var onAssembled = obj.GetType().GetMethod(method, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return onAssembled?.Invoke(obj, args);
        }

        private static List<Assembly> _assemblies = new List<Assembly>();
        public static Type GetType(string typeName)
        {
            Type type = null;

            foreach (Assembly assembly in _assemblies)
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            if (type == null)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    type = assembly.GetType(typeName);
                    if (type != null)
                    {
                        _assemblies.Add(assembly);
                        return type;
                    }
                }
            }

            throw new InvalidOperationException("Type '" + typeName + "' not location in any currently loaded assemblies.");
        }

        public static IEnumerable<Type> GetAllOfType(Type type)
            => GetAllOfType(type, AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.IsDynamic));

        public static IEnumerable<Type> GetAllOfTypeFromGameAssemblies(Type type)
            => GetAllOfType(type, GameAssemblies.Where(x => !x.IsDynamic));

        public static IEnumerable<Type> GetAllOfType(Type type, IEnumerable<Assembly> assemblies)
            => assemblies.SelectMany(x => x.GetExportedTypes()).Where(x => type.IsAssignableFrom(x));

        public static IEnumerable<T> InstantiateAllOfType<T>(params Type[] exclude)
            => InstantiateAllOfType<T>(GetAllOfType(typeof (T)), exclude);

        public static IEnumerable<T> InstantiateAllOfTypeFromGameAssemblies<T>(params Type[] exclude)
            => InstantiateAllOfType<T>(GetAllOfTypeFromGameAssemblies(typeof(T)), exclude);

        public static IEnumerable<T> InstantiateAllOfType<T> (IEnumerable<Type> allTypes, params Type[] exclude)
            => allTypes.Where(x => !x.IsAbstract && x.IsClass && !x.ContainsGenericParameters && !exclude.Contains(x)).Select(x => Activator.CreateInstance(x)).Cast<T>();
    }
}
