using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStatElement
    {
        object Owner { get; }
        float Value { get; }

        event Action OnChanged;
    }
}
