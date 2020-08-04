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

        public static object DynamicInvoke(object obj, string method, object[] args)
        {
            var onAssembled = obj.GetType().GetMethod(method, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            return onAssembled?.Invoke(obj, Array.Empty<object>());
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
    }
}
