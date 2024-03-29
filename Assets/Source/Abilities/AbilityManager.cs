using Lomztein.BFA2.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Abilities
{
    public class AbilityManager : MonoBehaviour
    {
        public static AbilityManager Instance;

        private List<Ability> _currentAbilities = new List<Ability>();
        public IEnumerable<Ability> CurrentAbilities => _currentAbilities;

        public event Action<Ability, object> OnAbilityActivated;

        private void Awake()
        {
            Instance = this;
        }

        public void AddAbility(Ability ability, object source)
        {
            _currentAbilities.Add(ability);
            OnAbilityAdded?.Invoke(ability, source);
            ability.OnActivated += Ability_OnActivated;
            ability.Initialize();
        }

        private void Ability_OnActivated(Ability arg1, object arg2)
        {
            OnAbilityActivated?.Invoke(arg1, arg2);
        }

        public bool RemoveAbility(string identifier, object source)
        {
            Ability current = GetAbility(identifier);
            if (_currentAbilities.Remove(current))
            {
                OnAbilityRemoved?.Invoke(identifier, source);
                current.OnActivated -= Ability_OnActivated;
                current.End();
                return true;
            }
            return false;
        }
        public Ability GetAbility(string identifier) => _currentAbilities.FirstOrDefault(x => x.Identifier.Equals(identifier));

        public event Action<Ability, object> OnAbilityAdded;
        public event Action<string, object> OnAbilityRemoved;
    }
}
