using Lomztein.BFA2.Serialization.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem
{
    public class ContentFileAccessor : IFileAccessor
    {
        public object LoadObjectFromFile(string path, Type type)
        {
            return Content.Get(path, type);
        }
    }
}
