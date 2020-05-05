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
        [ModelProperty]
        public float MaxHealth;
        [ModelProperty]
        public float Armor;
        [ModelProperty]
        public float Shields;
        [ModelProperty]
        public int Value;

        public float Health { get; private set; }
        public Color Color;

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
            GameObject.Find("PurchaseController").GetComponent<IResourceContainer>().ChangeResource(Resource.Credits, Value); // TODO: Figure out a better way to earn money from kills.
            Destroy(gameObject);
            _onDeath();
        }
    }
}
