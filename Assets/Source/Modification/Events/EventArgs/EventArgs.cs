using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events.EventArgs
{
    public class EventArgs<T> : IEventArgs
    {
        public T Object { get; private set; }

        public EventArgs (T obj)
        {
            Object = obj;
        }
    }
}
