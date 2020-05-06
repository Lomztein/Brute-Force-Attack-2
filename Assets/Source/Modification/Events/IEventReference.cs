using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventReference<T> where T : IEventArgs
    {
        IEvent<T> Event { get; }
    }
}
