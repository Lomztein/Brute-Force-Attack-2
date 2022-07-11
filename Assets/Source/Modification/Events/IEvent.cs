using System;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEvent
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }

        void AddListener(Action<EventArgs> listener, object source);
        void RemoveListener(Action<EventArgs> listener, object source);

        public event Action<IEvent, object> OnListenerAdded;
        public event Action<IEvent, object> OnListenerRemoved;

        void Execute(EventArgs args);
    }

    public interface IEvent<T> : IEvent where T : EventArgs
    {
        void AddListener(Action<T> listener);
        void RemoveListener(Action<T> listener, object source);

        void Execute(T args);
    }
}