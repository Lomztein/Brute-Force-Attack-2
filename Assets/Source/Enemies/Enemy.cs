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
        private const float CRITICAL_DAMAGE_MULT = 2f;

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

        [SerializeField]
        [ModelProperty]
        public float Value;

        public float Health { get; private set; }
        [ModelProperty]
        public Color Color;

        private IEnemyMotor _motor;

        public event Action<IEnemy> OnKilled;
        public event Action<IEnemy> OnFinished;

        private void Start()
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
            OnFinished?.Invoke(this);
        }

        public float TakeDamage(DamageInfo damageInfo)
        {
            float damage = GetDamage(damageInfo);
            float reduction = GetDamageReduction(damage);

            damageInfo.DamageDealt = Mathf.Min (Health + reduction, damageInfo.Damage);

            Health -= Mathf.Min (damage, Health);
            Shields = Mathf.Max (Shields - 1, 0);

            if (Health <= 0f)
            {
                Die();
            }
            return Health;
        }

        private float GetDamage (DamageInfo info)
        {
            float damage = info.Damage * (info.Color == Color ? CRITICAL_DAMAGE_MULT : 1f);
            damage = Mathf.Max(damage - GetDamageReduction(damage), damage / Mathf.Max(GetDamageReduction(damage), 1));
            return damage;
        }

        private float GetDamageReduction (float damage)
        {
            return Armor + Shields;
        }

        private void Die ()
        {
            Destroy(gameObject);
            OnKilled?.Invoke(this);
        }

        public void Init(Vector3 position)
        {
            transform.position = position;
        }
    }
}
