using Lomztein.BFA2.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public static class Content
    {
        public static string CustomContentPath => Path.Combine(Paths.PersistantData, "Content", "Custom");

        private static string _userContentPackPathOverride = null;
        public static string UserContentPackPath => string.IsNullOrEmpty(_userContentPackPathOverride) ? CustomContentPath : _userContentPackPathOverride;


        private const string CONTENT_MANAGER_TAG = "ContentManager";
        public const string PACK_WILDCARD = "*";

        private static ContentManager _manager;

        private static ContentManager GetManager ()
        {
            if (_manager == null)
            {
                _manager = GameObject.FindGameObjectWithTag(CONTENT_MANAGER_TAG).GetComponent<ContentManager>();
            }
            return _manager;
        }

        public static void SetUserContentPack(string path)
        {
            _userContentPackPathOverride = path;
        }

        public static object Get(string path, Type type) => GetManager().GetCacheOrLoadContent(path, type);
        public static object Load(string path, Type type) => GetManager().LoadContent(path, type);

        public static IEnumerable<object> GetAll(string path, Type type) => GetManager().GetCacheOrLoadAllContent(path, type);
        public static IEnumerable<object> LoadAll(string path, Type type) => GetManager().LoadAllContent(path, type);

        public static T Get<T>(string path) => (T)Get(path, typeof(T));
        public static IEnumerable<T> GetAll<T>(string path) => GetAll(path, typeof(T)).Cast<T>();

        public static void ClearCache() => GetManager().ClearCache();
        public static void ClearCache(string path) => GetManager().ClearCache(path);

        public static IEnumerable<string> QueryContentIndex(string pattern) => GetManager().QueryContentIndex(pattern);
        public static void ResetIndex() => GetManager().RebuildIndex();
    }
}
