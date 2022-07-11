using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStatContainer
    {
        event Action<IStatReference, object> OnStatAdded;
        event Action<IStatReference, object> OnStatRemoved;
        event Action<IStatReference, object> OnStatChanged;

        bool HasStat(string identifier);

        IStatReference AddStat(StatInfo info, float baseValue, object source);

        void RemoveStat(string identifier, object source);

        void AddStatElement(string identifier, IStatElement element, object source);

        void RemoveStatElement(string identifier, object owner, object source);

        IStatReference GetStat(string identifier);
    }
}