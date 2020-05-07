using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    public interface ITargetProvider
    {
        Transform GetTarget();
    }
}