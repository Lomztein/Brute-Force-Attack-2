using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem.Loaders.ContentLoaders
{
    public class TextLoaderStrategy : IContentLoaderStrategy
    {
        public bool CanLoad(Type type) => type == typeof(string);

        public object Load(string path, Type type)
            => File.ReadAllText(path);
    }
}
