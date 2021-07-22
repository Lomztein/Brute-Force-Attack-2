using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatContainer : IStatContainer
    {
        private List<IStat> _stats = new List<IStat>();

        public IStatReference AddStat(StatInfo info, float baseValue)
        {
            if (HasStat(info.Identifier))
            {
                RemoveStat(info.Identifier);
            }
            _stats.Add(new Stat(info.Identifier, info.Name, info.Description, baseValue, info.Function, info.Formatter));
            return new StatReference(GetStatInternal(info.Identifier));
        }

        public void AddStatElement(string identifier, IStatElement element)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.AddElement(element);
            }
        }

        private IStat GetStatInternal(string identifier) 
        {
            return _stats.FirstOrDefault(x => x.Identifier == identifier);
        }

        public IStatReference GetStat(string identifier)
        {
            return new StatReference(_stats.First(x => x.Identifier == identifier));
        }

        public bool HasStat(string identifier)
        {
            return _stats.Any(x => x.Identifier == identifier);
        }

        public void RemoveStat(string identifier)
        {
            _stats.RemoveAll(x => x.Identifier == identifier);
        }

        public void RemoveStatElement(string identifier, object owner)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.RemoveElement(owner);
            }
        }

        public override string ToString()
        {
            return _stats.Count != 0 ? string.Join("\n\t", _stats) : string.Empty;
        }
    }
}
