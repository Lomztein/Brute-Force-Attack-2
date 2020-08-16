using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public interface IStatContainer
    {
        bool HasStat(string identifier);

        IStatReference AddStat(string identifier, string name, string description, float baseValue);

        void RemoveStat(string identifier);

        void AddStatElement(string identifier, IStatElement element, Stat.Type type);

        void RemoveStatElement(string identifier, object owner, Stat.Type type);

        IStatReference GetStat(string identifier);
    }
}