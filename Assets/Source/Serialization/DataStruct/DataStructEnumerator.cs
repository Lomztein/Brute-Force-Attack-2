using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Serialization.DataStruct
{
    public class DataStructEnumerator : IEnumerator<object>
    {
        private IDataStruct ToEnumerate { get; set; }
        private int CurrentIndex { get; set; }

        public DataStructEnumerator(IDataStruct toEnumerate)
        {
            ToEnumerate = toEnumerate;
            Reset();
        }

        public object Current => ToEnumerate.GetValue<object>(CurrentIndex);

        public bool MoveNext()
        {
            CurrentIndex++;
            return CurrentIndex < ToEnumerate.Count;
        }

        public void Reset()
        {
            CurrentIndex = -1;
        }

        public void Dispose() { }
    }
}
