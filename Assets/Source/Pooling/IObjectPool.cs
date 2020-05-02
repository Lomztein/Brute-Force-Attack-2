using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Pooling
{
    public interface IObjectPool<T> where T : IPoolObject
    {
        T Get();

        void Insert(T obj);

        void Clear();

        event Action<T> OnNew;
    }
}
