using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatAggregate
    {
        public Stat.Type Type { get; private set; }
        private bool _hasChanged;
        private float _cache;

        private List<IStatElement> _elements = new List<IStatElement>();

        public StatAggregate (Stat.Type type)
        {
            Type = type;
        }

        public float GetValue()
        {
            if (_hasChanged)
            {
                _cache = CalcValue();
                _hasChanged = false;
            }
            return _cache;
        }

        private float CalcValue ()
        {
            return _elements.Sum(x => x.Value);
        }

        public void AddElement (IStatElement element)
        {
            if (_elements.Any (x => x.Owner == element.Owner))
            {
                throw new ArgumentException("The given elements owner already has a registered stat element.");
            }
            _elements.Add(element);
            _hasChanged = true;
        }

        public void RemoveElement (object owner)
        {
            IStatElement element = _elements.FirstOrDefault(x => x.Owner == owner);
            if (element != null)
            {
                _elements.Remove(element);
                _hasChanged = true;
            }
        }

    }
}