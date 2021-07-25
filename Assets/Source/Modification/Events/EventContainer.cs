using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventContainer : IEventContainer
    {
        private List<IEvent> _events = new List<IEvent>();

        public IEventCaller AddEvent(EventInfo info)
        {
            if (!HasEvent (info.Identifier))
            {
                _events.Add (new Event(info.Identifier, info.Name, info.Description));
            }
            return new EventCaller(FindEvent(info.Identifier) as IEvent);
        }

        public IEventReference GetEvent(string identifier)
        {
            if (HasEvent(identifier))
            {
                return new EventReference(FindEvent(identifier));
            }
            return null;
        }

        private IEvent FindEvent (string identifier)
        {
            return _events.First(x => x.Identifier == identifier);
        }

        public bool HasEvent (string identifier)
        {
            return _events.Any(x => x.Identifier == identifier);
        }
    }
}
