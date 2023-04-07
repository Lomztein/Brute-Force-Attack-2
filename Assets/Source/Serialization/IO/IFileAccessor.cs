using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.IO
{
    public interface IFileAccessor
    {
        public object LoadObjectFromFile(string path, Type type);

        public bool TryGetObjectFilePath(object obj, out string path);
    }
}
