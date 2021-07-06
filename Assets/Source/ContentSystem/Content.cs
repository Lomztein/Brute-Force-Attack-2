using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public static class Content
    {
        public static string CustomContentPath => Paths.PersistantData + "/Content/Custom/";

        private const string CONTENT_MANAGER_TAG = "ContentManager";
        private static ContentManager _manager;

        private static ContentManager GetManager ()
        {
            if (_manager == null)
            {
                _manager = GameObject.FindGameObjectWithTag(CONTENT_MANAGER_TAG).GetComponent<ContentManager>();
            }
            return _manager;
        }

        public static object Get(string path, Type type) => GetManager().GetContent(path, type, true);
        public static object Get(string path, Type type, bool useCache) => GetManager().GetContent(path, type, useCache);

        public static object[] GetAll(string path, Type type) => GetManager().GetAllContent(path, type, true);
        public static object[] GetAll(string path, Type type, bool useCache) => GetManager().GetAllContent(path, type, useCache);

        public static T Get<T>(string path) => (T)Get(path, typeof(T));
        public static T Get<T>(string path, bool useCache) => (T)Get(path, typeof(T), useCache);

        public static T[] GetAll<T>(string path) => GetAll(path, typeof(T)).Cast<T>().ToArray();
        public static T[] GetAll<T>(string path, bool useCache) => GetAll(path, typeof(T), useCache).Cast<T>().ToArray();

        public static void ClearCache(string path) => GetManager().ClearCache(path);
    }
}
