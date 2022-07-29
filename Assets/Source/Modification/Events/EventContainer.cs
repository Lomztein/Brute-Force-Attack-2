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

        public event Action<IEventReference, object> OnEventAdded;
        public event Action<IEventReference, object> OnEventChanged;
        public event Action<IEvent, object> OnEventExecuted;

        public IEventCaller AddEvent(EventInfo info, object source)
        {
            IEvent eve = null;
            if (!HasEvent (info.Identifier))
            {
                eve = new Event(info.Identifier, info.Name, info.Description);
                eve.OnListenerAdded += Eve_OnListenerAdded;
                eve.OnListenerRemoved += Eve_OnListenerRemoved;
                eve.OnExecute += Eve_OnExecute;
                OnEventAdded?.Invoke(new EventReference(eve), source);
                _events.Add (eve);
            }
            else
            {
                eve = FindEvent(info.Identifier);
            }
            return new EventCaller(eve);
        }

        private void Eve_OnExecute(IEvent arg1, object arg2)
        {
            OnEventExecuted?.Invoke(arg1, arg2);
        }

        private void Eve_OnListenerRemoved(IEvent arg1, object arg2)
        {
            OnEventChanged?.Invoke(new EventReference(arg1), arg2);
        }

        private void Eve_OnListenerAdded(IEvent arg1, object arg2)
        {
            OnEventChanged?.Invoke(new EventReference(arg1), arg2);
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
