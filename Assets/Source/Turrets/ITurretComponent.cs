using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretComponent
    {  
        ITurretAssembly Assembly { get; set; }
    }
}