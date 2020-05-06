using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventCaller<T> : IEventCaller<T> where T : IEventArgs
    {
        private IEvent<T> _event;

        public EventCaller(IEvent<T> @event)
        {
            _event = @event;
        }

        public void CallEvent(T args)
        {
            _event.Execute(args);
        }

    }
}
