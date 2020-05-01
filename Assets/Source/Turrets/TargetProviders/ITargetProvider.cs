using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    [TurretComponent]
    public interface ITargetProvider
    {
        Transform[] GetTargets();
    }
}