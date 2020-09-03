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

        public IStatReference AddStat(string identifier, string name, string description, float baseValue)
        {
            if (HasStat(identifier))
            {
                RemoveStat(identifier);
            }
            _stats.Add(new Stat(identifier, name, description, baseValue));
            return new StatReference(GetStatInternal(identifier));
        }

        public void AddStatElement(string identifier, IStatElement element, Stat.Type type)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.AddElement(element, type);
            }
            else
            {
                Debug.LogWarning("Tried to add stat element to non-existing stat: " + identifier);
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
            else
            {
                Debug.LogWarning("Tried to remove stat element to non-existing stat: " + identifier);
            }
        }
    }
}
