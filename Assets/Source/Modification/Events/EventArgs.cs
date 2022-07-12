using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventArgs
    {
        public object Origin { get; private set; }
        public object Args { get; private set; }

        public EventArgs (object origin, object args)
        {
            Origin = origin;
            Args = args;
        }

        public T GetOrigin<T>() => (T)Origin;
        public T GetArgs<T>() => (T)Args;

        public bool TryGetOrigin<T>(out T origin)
        {
            if (Origin is T)
            {
                origin = (T)Origin;
                return true;
            }
            origin = default;
            return false;
        }

        public bool TryGetArgs<T>(out T obj)
        {
            if (Args is T)
            {
                obj = (T)Args;
                return true;
            }
            obj = default;
            return false;
        }
    }
}
