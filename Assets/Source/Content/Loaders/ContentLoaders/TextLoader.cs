using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Content.Loaders.ContentLoaders
{
    public class TextLoader : IRawContentTypeLoader
    {
        public Type ContentType => typeof(string);

        public object Load(string path)
            => File.ReadAllText(path);
    }
}
