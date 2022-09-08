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

        public object Load(string path, Type type, IEnumerable<string> patches)
        {
            StringBuilder builder = new(File.ReadAllText(path));
            foreach (var pat in patches)
            {
                builder.Append(File.ReadAllText(pat));
            }
            return builder.ToString();
        }
    }
}
