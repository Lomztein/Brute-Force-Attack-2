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

        public event Action<IEvent, object> OnListenerAdded;
        public event Action<IEvent, object> OnListenerRemoved;

        private event Action<EventArgs> DoExecute;
        public event Action<IEvent, object> OnExecute;

        public Event (string identifier, string name, string description)
        {
            Identifier = identifier;
            Name = name;
            Description = description;
        }

        public void Execute(EventArgs args, object source)
        {
            DoExecute?.Invoke(args);
            OnExecute?.Invoke(this, source);
        }

        public void AddListener(Action<EventArgs> listener, object source)
        {
            DoExecute += listener;
            OnListenerAdded?.Invoke(this, source);
        }

        public void RemoveListener(Action<EventArgs> listener, object source)
        {
            DoExecute -= listener;
            OnListenerRemoved?.Invoke(this, source);
        }
    }
}