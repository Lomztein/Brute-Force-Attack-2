using Lomztein.BFA2.Plugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlugin
{
    void Start(Facade facade);

    void Stop(Facade facade);
}
