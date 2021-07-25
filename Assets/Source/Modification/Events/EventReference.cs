using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public class EventReference : IEventReference
    {
        public IEvent Event { get; private set; }

        public EventReference(IEvent @event)
        {
            Event = @event;
        }
    }
}