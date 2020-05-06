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

        public IEventCaller<T> AddEvent<T>(string identifier, string name, string description) where T : IEventArgs
        {
            if (!ContainsEvent (identifier))
            {
                IEvent<T> e = new Event<T>(identifier, name, description);
                _events.Add (e);
                return new EventCaller<T>(e);
            }
            throw new InvalidOperationException($"Event with {nameof(identifier)} already exists in this container.");
        }

        public IEventReference<T> GetEvent<T>(string identifier) where T : IEventArgs
        {
            return new EventReference<T>(FindEvent(identifier) as IEvent<T>);
        }

        private IEvent FindEvent (string identifier)
        {
            return _events.First(x => x.Identifier == identifier);
        }

        private bool ContainsEvent (string identifier)
        {
            return _events.Any(x => x.Identifier == identifier);
        }
    }
}
