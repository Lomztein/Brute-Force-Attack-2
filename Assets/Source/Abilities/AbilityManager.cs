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

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (RoundController.Instance)
            {
                RoundController.Instance.OnWaveFinished += Instance_OnWaveFinished;
            }
        }

        private void OnDestroy()
        {
            if (RoundController.Instance)
            {
                RoundController.Instance.OnWaveFinished -= Instance_OnWaveFinished;
            }
        }

        private void Instance_OnWaveFinished(int arg1, Enemies.Waves.WaveHandler arg2)
        {
            foreach (var ability in _currentAbilities)
            {
                ability.Cooldown(1);
            }
        }

        public void AddAbility(Ability ability, object source)
        {
            _currentAbilities.Add(ability);
            OnAbilityAdded?.Invoke(ability, source);
        }

        public bool RemoveAbility(string identifier, object source)
        {
            if (_currentAbilities.RemoveAll(x => x.Identifier.Equals(identifier)) > 0)
            {
                OnAbilityRemoved(identifier, source);
                return true;
            }
            return false;
        }
        public Ability GetAbility(string identifier) => _currentAbilities.FirstOrDefault(x => x.Identifier.Equals(identifier));

        public event Action<Ability, object> OnAbilityAdded;
        public event Action<string, object> OnAbilityRemoved;
    }
}
