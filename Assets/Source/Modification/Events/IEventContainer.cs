using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventContainer
    {
        event Action<IEventReference, object> OnEventAdded;
        event Action<IEventReference, object> OnEventChanged;

        IEventCaller AddEvent(EventInfo info, object source);

        IEventReference GetEvent(string identifier);

        bool HasEvent(string identifier);
    }
}