using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem
{
    /// <summary>
    /// Tracks a list of Unity objects that should be destroyed when cache is cleared.
    /// Cached objects that aren't disposed of elsewhere should be added to this.
    /// </summary>
    internal static class ContentCacheUnityObjectTracker
    {
        private static List<UnityEngine.Object> _cache = new List<UnityEngine.Object>();

        public static void AddObject(UnityEngine.Object obj)
        {
            _cache.Add(obj);
        }

        public static void RemoveObject(UnityEngine.Object obj)
        {
            _cache.Remove(obj);
        }

        public static void ClearCache ()
        {
            foreach (var obj in _cache)
            {
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
            _cache.Clear();
        }
    }
}
