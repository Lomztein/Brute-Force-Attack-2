using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Utilities
{
    public static class EnumerableExtensions 
    {
        public static IEnumerable<T> ObjectToEnumerable<T> (this T obj)
        {
            yield return obj;
        }
    }
}
