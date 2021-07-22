using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStatContainer
    {
        bool HasStat(string identifier);

        IStatReference AddStat(StatInfo info, float baseValue);

        void RemoveStat(string identifier);

        void AddStatElement(string identifier, IStatElement element);

        void RemoveStatElement(string identifier, object owner);

        IStatReference GetStat(string identifier);
    }
}