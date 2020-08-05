using Lomztein.BFA2.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets
{
    public interface ITurretComponent : IIdentifiable
    {  
        Grid.Size Width { get; }
        Grid.Size Height { get; }
    }
}