using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.DataStruct
{
    public abstract class DataStructBase : IDataStruct
    {
        public abstract int Count { get; }
        public abstract bool IsNull { get; }

        public virtual void Dispose() { }

        public abstract IDataStruct Get(object identifier);

        public abstract T GetValue<T>(object identifier);

        public IEnumerator GetEnumerator()
        {
            return new DataStructEnumerator(this);
        }

        public abstract T ToObject<T>();
    }
}
