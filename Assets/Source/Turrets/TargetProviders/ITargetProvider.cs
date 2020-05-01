using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    public interface ITargetProvider
    {
        Transform[] GetTargets();
    }
}