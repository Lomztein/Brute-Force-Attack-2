using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventContainer
    {
        IEvent[] Events { get; }

        event Action<IEventReference, object> OnEventAdded;
        event Action<IEventReference, object> OnEventChanged;
        event Action<IEvent, object> OnEventExecuted;

        IEventCaller AddEvent(EventInfo info, object source);

        IEventReference GetEvent(string identifier);

        bool HasEvent(string identifier);
    }
}