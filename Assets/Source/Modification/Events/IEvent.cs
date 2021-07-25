using System;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEvent
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }

        event Action<EventArgs> OnExecute;

        void Execute(EventArgs args);
    }

    public interface IEvent<T> : IEvent where T : EventArgs
    {
        new event Action<T> OnExecute;

        void Execute(T args);
    }
}