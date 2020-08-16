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

        void AddElement(IStatElement element, Stat.Type type);

        void RemoveElement(object owner, Stat.Type type);
        
        float GetValue();
    }
}