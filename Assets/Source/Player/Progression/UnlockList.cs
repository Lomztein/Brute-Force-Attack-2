using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression
{
    public class UnlockList : MonoBehaviour, IUnlockList
    {
        [SerializeField] private string _name;
        public string Name => _name;

        private Dictionary<string, bool> _unlocks = new Dictionary<string, bool>();

        public event Action<string, bool> OnUnlockChange;

        public bool IsUnlocked(string identifier) => _unlocks.ContainsKey(identifier) ? _unlocks[identifier] : false;

        public void Add(string identifier, bool unlocked)
        {
            if (_unlocks.ContainsKey(identifier))
            {
                _unlocks.Add(identifier, unlocked);
            }
        }

        public void SetUnlocked(string identifier, bool value)
        {
            if (!_unlocks.ContainsKey(identifier))
            {
                Add(identifier, value);
            }
            _unlocks[identifier] = value;
            OnUnlockChange?.Invoke(identifier, value);
        }

        public void Unlock(string identifier) => SetUnlocked(identifier, true);

        public void Lock(string identifier) => SetUnlocked(identifier, false);
    }
}