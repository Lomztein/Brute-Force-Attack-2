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

        public event Action<IStatReference, object> OnStatAdded;
        public event Action<IStatReference, object> OnStatRemoved;
        public event Action<IStatReference, object> OnStatChanged;

        public IStatReference AddStat(StatInfo info, float baseValue, object source)
        {
            if (HasStat(info.Identifier))
            {
                RemoveStat(info.Identifier, source);
            }
            _stats.Add(new Stat(info.Identifier, info.Name, info.Description, baseValue, info.Function, info.Formatter));
            IStatReference reference = new StatReference(GetStatInternal(info.Identifier));
            OnStatAdded?.Invoke(reference, source);
            return reference;
        }

        public void AddStatElement(string identifier, IStatElement element, object source)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.AddElement(element);
                OnStatChanged?.Invoke(new StatReference(stat), source);
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

        public void RemoveStat(string identifier, object source)
        {
            IStat stat = GetStatInternal(identifier);
            if (_stats.RemoveAll(x => x.Identifier == identifier) > 0)
            {
                OnStatRemoved?.Invoke(new StatReference(stat), source);
            }
        }

        public void RemoveStatElement(string identifier, object owner, object source)
        {
            IStat stat = GetStatInternal(identifier);
            if (stat != null)
            {
                stat.RemoveElement(owner);
                OnStatChanged?.Invoke(new StatReference(stat), source);
            }
        }

        public override string ToString()
        {
            return _stats.Count != 0 ? string.Join("\n\t", _stats) : string.Empty;
        }
    }
}
