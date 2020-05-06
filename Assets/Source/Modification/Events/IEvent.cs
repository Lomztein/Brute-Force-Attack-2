using System;

namespace Lomztein.BFA2.Modification.Events
{
    public interface IEvent
    {
        string Identifier { get; }
        string Name { get; }
        string Description { get; }

        event Action<IEventArgs> OnExecute;

        void Execute(IEventArgs args);
    }

    public interface IEvent<T> : IEvent where T : IEventArgs
    {
        new event Action<T> OnExecute;

        void Execute(T args);
    }
}