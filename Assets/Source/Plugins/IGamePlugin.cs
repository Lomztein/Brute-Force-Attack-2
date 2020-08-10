using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Plugins
{
    public interface IGamePlugin
    {
        void Start();

        void Stop();
    }
}
