using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventReference<T> : IEventReference<T> where T : IEventArgs
    {
        public IEvent<T> Event { get; private set; }

        public EventReference(IEvent<T> @event)
        {
            Event = @event;
        }
    }
}