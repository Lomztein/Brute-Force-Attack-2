using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Events
{
    public class Event : IEvent
    {
        public string Identifier { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public event Action<IEventArgs> OnExecute;

        public Event (string identifier, string name, string description)
        {
            Identifier = identifier;
            Name = name;
            Description = description;
        }

        public void Execute(IEventArgs args) => OnExecute?.Invoke(args);
    }

    public class Event<T> : Event, IEvent<T> where T : IEventArgs
    {
        public Event(string identifier, string name, string description) : base(identifier, name, description)
        {
        }

        public new event Action<T> OnExecute;

        public void Execute(T args) => OnExecute?.Invoke(args);
    }
}