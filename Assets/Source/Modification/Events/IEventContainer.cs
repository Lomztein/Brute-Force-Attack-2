using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventContainer
    {
        IEventCaller AddEvent(EventInfo info);

        IEventReference GetEvent(string identifier);

        bool HasEvent(string identifier);
    }
}