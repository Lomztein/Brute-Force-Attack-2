using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventCaller : IEventCaller
    {
        private IEvent _event;

        public EventCaller(IEvent @event)
        {
            _event = @event;
        }

        public void CallEvent(EventArgs args)
        {
            _event.Execute(args);
        }

    }
}
