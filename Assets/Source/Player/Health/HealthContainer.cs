using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class HealthContainer : MonoBehaviour, IHealthContainer
    {
        [ModelProperty]
        public float MaxHealth;
        [ModelProperty]
        public float StartingHealth;

        private float _health;
        private bool _exhausted;

        public event Action<float, float, float, object> OnHealthChanged;
        public event Action<object> OnHealthExhausted;

        private void Awake()
        {
            _health = StartingHealth;
        }

        private void Start()
        {
            ChangeHealth(0, this);
        }

        public float ChangeHealth(float amount, object source)
        {
            float prev = _health;
            _health += amount;
            OnHealthChanged?.Invoke(prev, _health, MaxHealth, source);

            if (!_exhausted && _health <= 0f)
            {
                Die(source);
            }

            return _health;
        }

        private void Die (object source)
        {
            _exhausted = true;
            OnHealthExhausted?.Invoke(source);
        }

        public float GetMaxHealth() => MaxHealth;

        public float GetCurrentHealth()
        {
            return _health;
        }
    }
}