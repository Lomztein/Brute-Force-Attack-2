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
        private const string CONTENT_MANAGER_TAG = "ContentManager";
        private static IContentManager _manager;

        private static IContentManager GetManager ()
        {
            if (_manager == null)
            {
                _manager = GameObject.FindGameObjectWithTag(CONTENT_MANAGER_TAG).GetComponent<IContentManager>();
            }
            return _manager;
        }

        public static object Get(string path, Type type) => GetManager().GetContent(path, type);

        public static object[] GetAll(string path, Type type) => GetManager().GetAllContent(path, type);

        public static T Get<T>(string path) => (T)Get(path, typeof(T));

        public static T[] GetAll<T>(string path) => GetAll(path, typeof(T)).Cast<T>().ToArray();
    }
}
