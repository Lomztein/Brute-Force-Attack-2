using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEventContainer
    {
        IEventCaller<T> AddEvent<T>(string identifier, string name, string description) where T : IEventArgs;

        IEventReference<T> GetEvent<T>(string identifier) where T : IEventArgs;
    }
}