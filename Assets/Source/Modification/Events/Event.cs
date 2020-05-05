using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : IEvent
{
    public event Action<IEventArgs> OnExecute;

    public void Execute(IEventArgs args) => OnExecute?.Invoke(args);
}
