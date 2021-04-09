using Lomztein.BFA2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Plugins
{
    public interface IPlugin
    {
        void Start(Facade facade);

        void Stop(Facade facade);
    }
}