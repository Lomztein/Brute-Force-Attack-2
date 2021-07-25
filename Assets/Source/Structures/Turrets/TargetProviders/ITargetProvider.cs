using Lomztein.BFA2.Modification.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.TargetProviders
{
    public interface ITargetProvider
    {
        Transform GetTarget();
    }
}