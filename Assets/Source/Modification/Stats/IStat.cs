using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStat
    {
        string Name { get; }
        string Description { get; }
        string Identifier { get; }

        void AddElement(IStatElement element);
        void RemoveElement(object owner);
        
        float GetValue();

        event Action OnChanged;
    }
}