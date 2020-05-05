using System;

public interface IEvent
{
    event Action<IEventArgs> OnExecute;

    void Execute(IEventArgs args);
}