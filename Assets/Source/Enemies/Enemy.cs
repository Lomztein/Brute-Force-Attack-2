using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Enemies.Motors;
using Lomztein.BFA2.Enemies.Waves;
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

        [ModelProperty]
        public float MaxHealth;
        [ModelProperty]
        public float Armor;
        [ModelProperty]
        public float Shields;
        [ModelProperty]
        public float MaxSpeed;

        public Vector3[] Path { get => _motor.Path; set => _motor.Path = value; }
        public float Speed { get => _motor.Speed; set => _motor.Speed = value; }
        public int PathIndex { get => _motor.PathIndex; set => _motor.PathIndex = value; }

        public float Health;
        private bool _isDead;

        [ModelProperty]
        public string _UniqueIdentifier;
        public string Identifier => _UniqueIdentifier;

        [ModelProperty]
        public Color Color;
        public IEnemyMotor _motor;

        [ModelProperty]
        public float DeathParticleLife;
        private ParticleSystem _deathParticle;

        public event Action<IEnemy> OnKilled;
        public event Action<IEnemy> OnFinished;

        private void FixedUpdate()
        {
            _motor.Tick(Time.fixedDeltaTime);
            if (_motor.HasReachedEnd ())
            {
                DoDamage();
            }
            else
            {
                Accelerate(Time.fixedDeltaTime);
            }
        }

        private void Accelerate(float deltaTime)
        {
            if (Speed < MaxSpeed)
            {
                Speed += 1f / (MaxSpeed * deltaTime);
            }
            else
            {
                Speed = MaxSpeed;
            }
        }

        private void DoDamage()
        {
            if (!_isDead)
            {
                Destroy(gameObject);
                _isDead = true;
                OnFinished?.Invoke(this);
            }
        }

        public float TakeDamage(DamageInfo damageInfo)
        {
            float damage = GetDamage(damageInfo);
            float reduction = GetDamageReduction(damage);

            damageInfo.DamageDealt = Mathf.Min (Health + reduction, damageInfo.Damage);

            Health -= Mathf.Min (damage, Health);
            Shields = Mathf.Max (Shields - 1, 0);

            if (Health <= 0f && !_isDead)
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

            _deathParticle.transform.parent = null;
            _deathParticle.Play();
            _isDead = true;

            Destroy(_deathParticle.gameObject, DeathParticleLife);
        }

        public void Init(Vector3 position, Vector3[] path)
        {
            Health = MaxHealth;
            _motor = GetComponent<IEnemyMotor>();
            _deathParticle = transform.Find("DeathParticle").GetComponent<ParticleSystem>();
            Speed = MaxSpeed;

            transform.position = position;
            _motor.Path = path;
        }
    }
}
