using System;
using System.Collections.Generic;
using UnityEditor;

namespace Util.Editor
{
    public static class SerializedPropertyHelper
    {
        private static readonly Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();
        
        public static Type GetPropertyFieldType(SerializedProperty property) => 
            GetTypeByName(property.managedReferenceFieldTypename);

        public static Type GetPropertyObjectType(SerializedProperty property) => 
            GetTypeByName(property.managedReferenceFullTypename);

        public static bool IsListType(System.Type tp)
        {
            if (tp == null) return false;
            if (tp.IsArray) return tp.GetArrayRank() == 1;
            var interfaces = tp.GetInterfaces();
            if (Array.IndexOf(interfaces, typeof(System.Collections.IList)) >= 0 || Array.IndexOf(interfaces, typeof(IList<>)) >= 0) {
                return true;
            }
            return false;
        }

        public static Type GetElementTypeOfListType(Type tp)
        {
            if (tp == null) return null;

            if (tp.IsArray) return tp.GetElementType();

            var interfaces = tp.GetInterfaces();
            //if (interfaces.Contains(typeof(System.Collections.IList)) || interfaces.Contains(typeof(IList<>)))
            if (Array.IndexOf(interfaces, typeof(System.Collections.IList)) >= 0 || Array.IndexOf(interfaces, typeof(IList<>)) >= 0) {
                return tp.IsGenericType ? tp.GetGenericArguments()[0] : typeof(object);
            }
            return null;
        }
    
        public static Type GetTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;
            
            if (TypeCache.TryGetValue(typeName, out var value))
                return value;

            var typeSplit = typeName.Split(char.Parse(" "));
            var typeAssembly = typeSplit[0];
            var typeClass = typeSplit[1];

            var type = Type.GetType(typeClass + ", " + typeAssembly);
            TypeCache[typeName] = type;
            return type;
        }
    }
}