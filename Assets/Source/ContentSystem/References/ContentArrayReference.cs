using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.ContentSystem.References
{
    public class ContentArrayReference<T> : IEnumerable<T>
    {
        private T[] _cache;
        public string Path;

        public ContentArrayReference (string path)
        {
            Path = path;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)GetCache()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetCache().GetEnumerator();
        }

        private T[] GetCache ()
        {
            if (_cache == null)
            {
                _cache = Content.GetAll<T>(Path);
            }
            return _cache;
        }
    }
}
