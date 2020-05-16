using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies.Motors;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy, IDamagable
    {
        [ModelProperty] [SerializeField]
        private float _difficultyValue;
        public float DifficultyValue => _difficultyValue;
        [ModelProperty]
        public float MaxHealth;
        [ModelProperty]
        public float Armor;
        [ModelProperty]
        public float Shields;
        [ModelProperty]
        public float Damage;

        [SerializeField][ModelProperty]
        private int _value;
        public int Value { get => _value; }

        public float Health { get; private set; }
        public Color Color;

        private IEnemyMotor _motor;

        public event Action<IEnemy, int> OnDeath;
        public event Action<IEnemy, float> OnFinished;

        private void Awake()
        {
            Health = MaxHealth;
            _motor = GetComponent<IEnemyMotor>();
        }

        private void FixedUpdate()
        {
            _motor.Tick(Time.fixedDeltaTime);
            if (_motor.HasReachedEnded ())
            {
                DoDamage();
            }
        }

        private void DoDamage ()
        {
            Destroy(gameObject);
            OnFinished?.Invoke(this, Damage);
        }

        public float TakeDamage(DamageInfo damageInfo)
        {
            float damage = GetDamage(damageInfo);
            damageInfo.DamageDealt = Mathf.Min (Health + GetDamageReduction (damage), damageInfo.Damage);

            Health -= Mathf.Min (damage, Health);
            Shields--;

            if (Health <= 0f)
            {
                Die();
            }
            return Health;
        }

        private float GetDamage (DamageInfo info)
        {
            return Mathf.Max(info.Damage - GetDamageReduction(info.Damage), 0f);
        }

        private float GetDamageReduction (float damage)
        {
            return Armor + Shields;
        }

        private void Die ()
        {
            Destroy(gameObject);
            OnDeath?.Invoke(this, Value);
        }

        public void Init(Vector3 position)
        {
            transform.position = position;
        }
    }
}
