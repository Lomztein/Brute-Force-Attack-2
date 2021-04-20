using Lomztein.BFA2.ContentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public static class SerializationFileAccess
    {
        private static IFileAccessor _accessor = new ContentFileAccessor();

        internal static void SetAccessor(IFileAccessor accessor) => _accessor = accessor;

        public static object LoadObjectFromFile(string path, Type type) => _accessor.LoadObjectFromFile(path, type);
        public static T LoadObjectFromFile<T>(string path) => (T)LoadObjectFromFile(path, typeof(T));
    }
}
