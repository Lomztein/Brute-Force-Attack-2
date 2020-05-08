using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.DataStruct
{
    public interface IDataStruct : IEnumerable<IDataStruct>, IDisposable
    {
        int Count { get; }
        bool IsNull { get; }

        IDataStruct Get(object identifier);
        object GetValue(object identifier, Type type);
        
        object ToObject(Type type);
    }

    public static class DataStructExtensions
    {
        public static T GetValue<T>(this IDataStruct data, object identifier)
            => (T)data.GetValue(identifier, typeof(T));

        public static T ToObject<T>(this IDataStruct data)
            => (T)data.ToObject(typeof(T));
    }
}
