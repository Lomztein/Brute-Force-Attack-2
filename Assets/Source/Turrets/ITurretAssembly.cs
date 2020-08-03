using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretAssembly
    {
        string Name { get; set; }
        string Description { get; set; }
        bool Enabled { get; }

        ITurretComponent[] GetComponents();
        ITurretComponent GetRootComponent();

        void Heat(float amount);
    }
}