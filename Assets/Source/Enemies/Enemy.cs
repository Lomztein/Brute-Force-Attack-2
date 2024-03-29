﻿using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Enemies.Buffs;
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
    public class Enemy : MonoBehaviour, IIdentifiable, IDamagable
    {
        public const string ENEMIES_CCONTENT_PATH = "*/Enemies/*";

        private const float CRITICAL_DAMAGE_MULT = 2f;

        [ModelProperty]
        public double MaxHealth;
        [ModelProperty]
        public float Armor;
        [ModelProperty]
        public float Shields;
        [ModelProperty]
        public float MaxSpeed;

        public WaveHandler WaveHandler { get; set; }
        public Vector3[] Path { get => _motor.Path; set => _motor.Path = value; }
        public float Speed { get => _motor.Speed; set => _motor.Speed = value; }
        public int PathIndex { get => _motor.PathIndex; set => _motor.PathIndex = value; }

        public double Health { get; private set; }
        private bool _isDead;
        public DamageInfo LastDamageTaken;

        [ModelProperty]
        public string _UniqueIdentifier;
        public string Identifier => _UniqueIdentifier;

        [ModelProperty]
        public Color Color;
        public IEnemyMotor _motor;

        [ModelProperty]
        public float DeathParticleLife;
        private ParticleSystem _deathParticle;

        public event Action<Enemy> OnKilled;
        public event Action<Enemy> OnFinished;

        private List<EnemyBuff> _buffs = new List<EnemyBuff>();
        private Queue<EnemyBuff> _toRemove = new Queue<EnemyBuff>();

        public IEnumerable<EnemyBuff> Buffs => _buffs;

        private void Awake()
        {
            Health = MaxHealth;
        }

        private void FixedUpdate()
        {
            if (_motor == null)
            {
                Die(); // TODO: Fix me, appears that an enemy is sometimes spawned but not initialized at the end of a wave. This is a temporary workaround.
            }
            else
            {
                _motor.Tick(Time.fixedDeltaTime);

                if (_motor.HasReachedEnd())
                {
                    DoDamage();
                }
                else
                {
                    Accelerate(Time.fixedDeltaTime);
                }
            }

            foreach (EnemyBuff buff in _buffs)
            {
                buff.Tick(Time.fixedDeltaTime);
            }
            //EndBuffToRemoveQueue();
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                ClearBuffs();
                EndBuffToRemoveQueue();
            }
        }

        private void EndBuffToRemoveQueue ()
        {
            while (_toRemove.Count > 0)
            {
                EnemyBuff toRemove = _toRemove.Dequeue();
                toRemove.End();
                _buffs.Remove(toRemove);
            }
        }

        public bool TryAddBuff(EnemyBuff buff, int stackSize = 1) => TryAddBuff(buff, buff.Time, stackSize);

        public bool TryAddBuff (EnemyBuff buff, float buffTime, int stackSize = 1)
        {
            if (HasBuff(buff.Identifier) > 0)
            {
                EnemyBuff existing = GetBuff(buff.Identifier);
                int room = existing.CanAddStack(stackSize);
                if (room > 0)
                {
                    // Reset buff time to the highest of it's current or the time given here. 
                    // Individual timers for inidivual stacks could be nice, but I reckon this will work just fine.
                    existing.Time = Mathf.Max(buffTime, buff.Time);
                    existing.AddStack(Mathf.Min(room, stackSize));
                    return true;
                }
                return false;
            }
            else
            {
                _buffs.Add(buff);
                int room = buff.CanAddStack(stackSize);
                buff.OnTimeOut += Buff_OnTimeOut;
                buff.Begin(this, Mathf.Min(room, stackSize), buffTime);
                return true;
            }
        }

        private void Buff_OnTimeOut(EnemyBuff obj)
        {
            RemoveBuffInternal(obj, obj.CurrentStack);
        }

        private bool RemoveBuffInternal (EnemyBuff buff, int stackSize)
        {
            if (buff != null)
            {
                if (buff.CurrentStack > stackSize)
                {
                    buff.RemoveStack(stackSize);
                }
                else
                {
                    _toRemove.Enqueue(buff);
                }
                return true;
            }
            return false;
        }

        public bool RemoveBuff(string identifier, int stackSize = 1)
        {
            EnemyBuff buff = GetBuff(identifier);
            return RemoveBuffInternal(buff, stackSize);
        }

        public int HasBuff (string buffIdentifier)
        {
            EnemyBuff buff = GetBuff(buffIdentifier);
            if (buff == null) return 0;
            return buff.CurrentStack;
        }

        public EnemyBuff GetBuff(string buffIdentifier) => _buffs.FirstOrDefault(x => x.Identifier == buffIdentifier);

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
                ClearBuffs();
            }
        }

        public double TakeDamage(DamageInfo damageInfo)
        {
            double before = Health;
            double damage = GetDamage(damageInfo);

            LastDamageTaken = damageInfo;

            Health -= Math.Min (damage, Health);
            Shields = Math.Max (Shields - 1, 0);
            damageInfo.DamageDealt = Math.Min(before - Health, MaxHealth) + GetDamageReduction(damageInfo.Damage);

            if (Health <= 0f && !_isDead)
            {
                Die();
            }
            return Health;
        }

        public double Heal (double amount)
        {
            Health += amount;
            if (Health > MaxHealth)
                Health = MaxHealth;
            return Health;
        }

        private double GetDamage (DamageInfo info)
        {
            double damage = info.Damage * (info.Color == Color ? CRITICAL_DAMAGE_MULT : 1.0);
            damage = Math.Max(damage - GetDamageReduction(damage), damage / Math.Max(GetDamageReduction(damage), 1));
            return damage;
        }

        private double GetDamageReduction (double damage)
        {
            return Armor + Shields;
        }

        private void Die ()
        {
            Destroy(gameObject);
            OnKilled?.Invoke(this);
            _isDead = true;

            if (_deathParticle)
            {
                _deathParticle.transform.parent = null;
                _deathParticle.Play();
                Destroy(_deathParticle.gameObject, DeathParticleLife);
            }

            ClearBuffs();
        }

        private void ClearBuffs ()
        {
            foreach (EnemyBuff buff in _buffs)
            {
                buff.End();
                _toRemove.Enqueue(buff);
            }
        }

        public void Init(Vector3 position, Vector3[] path, WaveHandler handler)
        {
            WaveHandler = handler;
            Health = MaxHealth;
            _motor = GetComponent<IEnemyMotor>();
            _deathParticle = transform.Find("DeathParticle").GetComponent<ParticleSystem>();
            Speed = MaxSpeed;

            transform.position = position;
            _motor.Path = path;
        }

        public static IContentCachedPrefab[] GetEnemies() => Content.GetAll<IContentCachedPrefab>(ENEMIES_CCONTENT_PATH).ToArray();
        public static IContentCachedPrefab GetEnemy(string identifier) => GetEnemies().FirstOrDefault(x => x.GetCache().GetComponent<Enemy>().Identifier == identifier);
    }
}
