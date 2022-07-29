using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Stats
{
    public class StatReference : IStatReference
    {
        private IStat _stat;

        public string Identifier => _stat.Identifier;

        public StatReference (IStat stat)
        {
            _stat = stat;
            _stat.OnChanged += OnStatChanged;
        }

        private void OnStatChanged()
        {
            OnChanged?.Invoke();
        }

        public event Action OnChanged;

        public float GetValue ()
        {
            return _stat.GetValue();
        }
    }
}