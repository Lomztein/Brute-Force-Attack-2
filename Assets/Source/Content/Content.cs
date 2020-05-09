using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content
{
    public static class Content
    {
        private static IContentManager _manager = new ContentManager();

        public static void Init() => _manager.Init();

        public static object Get(string path, Type type)
            => _manager.GetContent(path, type);

        public static object[] GetAll (string path, Type type)
            => _manager.GetAllContent(path, type);
    }
}
