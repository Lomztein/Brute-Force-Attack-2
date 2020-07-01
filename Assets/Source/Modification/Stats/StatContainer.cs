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

        public IStatReference AddStat(string identifier, string name, string description)
        {
            if (!HasStat(identifier))
            {
                _stats.Add(new Stat(identifier, name, description));
            }
            return new StatReference(GetStatInternal(identifier));
        }

        public void AddStatElement(string identifier, IStatElement element, Stat.Type type)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.AddElement(element, type);
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

        public void RemoveStatElement(string identifier, object owner, Stat.Type type)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.RemoveElement(owner, type);
            }
        }

        public void Init(IStatBaseValues baseValues)
        {
            foreach (IStat stat in _stats)
            {
                stat.Init(baseValues.GetBaseValue(stat.Identifier));
            }
        }
    }
}
