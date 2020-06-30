using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretComponent
    {  
        ITurretAssembly Assembly { get; set; }

        string UniqueIdentifier { get; }

        Grid.Size Width { get; }
        Grid.Size Height { get; }
    }
}