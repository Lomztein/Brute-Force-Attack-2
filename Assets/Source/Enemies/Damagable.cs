using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class Damagable : MonoBehaviour, IDamagable
    {
        [ModelProperty]
        public double MaxHealth;
        private double _health;
        [ModelProperty]
        public Colorization.Color Color;
        private bool _isDead;
        [ModelProperty]
        public bool DestroyOnDeath;

        private void Awake()
        {
            _health = MaxHealth;
        }

        public double TakeDamage(DamageInfo damageInfo)
        {
            double before = _health;
            if (damageInfo.Color == Color)
            {
                _health -= damageInfo.Damage;
            }
            else
            {
                _health -= damageInfo.Damage / 2f;
            }
            damageInfo.DamageDealt = Math.Max(before - _health, MaxHealth);
            if (_health < 0 && !_isDead)
            {
                _isDead = true;
                if (DestroyOnDeath)
                {
                    Destroy(gameObject);
                }
            }

            return _health;
        }
    }
}
