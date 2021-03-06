﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Misc
{
    public interface IProvider<T>
    {
        T[] Get();
    }

    public interface IDynamicProvider<T> : IProvider<T>
    {
        event Action<IEnumerable<T>> OnAdded;
        event Action<IEnumerable<T>> OnRemoved;
    }
}
