using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.DataStruct
{
    public interface IDataStruct : IEnumerable, IDisposable
    {
        int Count { get; }
        bool IsNull { get; }

        IDataStruct Get(object identifier);
        T GetValue<T>(object identifier);
        T ToObject<T>();
    }
}
