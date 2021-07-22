using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatAggregate
    {
        private bool _hasChanged = true;
        private float _cache;

        private List<IStatElement> _elements = new List<IStatElement>();

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
            _elements.Add(element);
            _hasChanged = true;
        }

        public IStatElement RemoveElement (object owner)
        {
            IStatElement element = _elements.FirstOrDefault(x => x.Owner == owner);
            if (element != null)
            {
                _elements.Remove(element);
                _hasChanged = true;
            }
            return element;
        }

    }
}