using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretAssembly
    {
        bool Enabled { get; }

        ITurretComponent[] GetComponents();

        void Heat(float amount);
    }
}