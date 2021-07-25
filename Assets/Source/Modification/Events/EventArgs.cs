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
        public T GetObject<T>() => (T)Args;
    }
}
