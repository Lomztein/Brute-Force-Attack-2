using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Targeters
{
    [TurretComponent]
    public interface ITargeter
    {
        float GetDistance();
    }
}