using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies.Motors;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy, IDamagable
    {
        [ModelProperty]
        public float MaxHealth;
        [ModelProperty]
        public float Armor;
        public float Health { get; private set; }
        public ColorCache Color;

        private IEnemyMotor _motor;
        private Action _onDeath;

        private void Awake()
        {
            Health = MaxHealth;
            _motor = GetComponent<IEnemyMotor>();
            Invoke("Die", 10);
        }

        private void FixedUpdate()
        {
            _motor.Tick(Time.fixedDeltaTime);
        }

        public float TakeDamage(DamageInfo damageInfo)
        {
            float damage = Mathf.Max (damageInfo.Damage - Armor, 0f);
            float damageTaken = Mathf.Max(Health, damage);
            Health -= damage;
            damageInfo.DamageDealt = damageTaken;
            if (Health <= 0f)
            {
                Die();
            }
            return Health;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetOnDeathCallback(Action onDeath)
        {
            _onDeath = onDeath;
        }

        private void Die ()
        {
            Destroy(gameObject);
            _onDeath();
        }
    }
}
