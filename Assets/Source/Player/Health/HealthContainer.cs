using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class HealthContainer : MonoBehaviour, IHealthContainer
    {
        public float MaxHealth;
        public float StartingHealth;
        private float _health;

        private void Awake()
        {
            _health = StartingHealth;
        }

        public float ChangeHealth(float amount)
        {
            _health += amount;
            return _health;
        }

        public float GetMaxHealth() => MaxHealth;

        public float GetCurrentHealth()
        {
            return _health;
        }
    }
}