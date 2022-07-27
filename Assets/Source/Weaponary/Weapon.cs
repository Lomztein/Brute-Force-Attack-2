using Lomztein.BFA2.Animation;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary.FireSequence;
using Lomztein.BFA2.Weaponary.FireControl;
using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        private const string MUZZLE_PARENT_NAME = "Muzzles";

        public float Damage { get; set; }
        public float Firerate { get; set; }
        public int ProjectileAmount { get; set; }
        public float Range { get; set; }
        public float Speed { get; set; }
        public float Spread { get; set; }
        public float Pierce { get; set; }
        public int MuzzleCount => _muzzles.Length;

        public Transform Target { get; set; }
        public Colorization.Color Color { get; set; }
        public float Cooldown => 1f / Firerate;

        private IObjectPool<IProjectile> _pool;
        private IProjectilePool _projectilePool;
        private IAnimation _fireAnimation;
        private IFireSequence _fireSequence;
        private readonly List<IFireControl> _fireControl = new List<IFireControl>();

        private Transform[] _muzzles;
        private ParticleSystem[] _fireParticles;

        public event Action<IProjectile[]> OnFire;
        public event Action<IProjectile> OnProjectile;
        public event Action<HitInfo> OnProjectileDepleted;
        public event Action<HitInfo> OnProjectileHit;
        public event Action<HitInfo> OnProjectileKill;

        [ModelProperty]
        public ContentPrefabReference ProjectilePrefab;
        [ModelProperty]
        public LayerMask HitLayer;
        private bool _chambered;

        private bool _initialized;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (!_initialized)
            {
                _muzzles = GetMuzzles();
                _fireParticles = GetFireParticles(_muzzles);

                _fireAnimation = GetComponent<IAnimation>() ?? new NoAnimation();
                _fireSequence = GetComponent<IFireSequence>() ?? new InstantFireSequence();

                if (TryGetComponent(out IFireControl ctrl))
                {
                    AddFireControl(ctrl);
                }

                _pool = new NoGameObjectPool<IProjectile>(ProjectilePrefab);
                _projectilePool = new ProjectilePool(_pool);
                _pool.OnNew += OnNewProjectile;

                if (isActiveAndEnabled)
                {
                    _chambered = false;
                    StartCoroutine(Rechamber(Cooldown));
                }
                else
                {
                    _chambered = true;
                }
                _initialized = true;
            }
        }

        private IEnumerator Rechamber(float cooldown)
        {
            yield return UnityUtils.WaitForFixedSeconds(cooldown);
            _chambered = true;
        }

        public float GetDamage() => Damage;
        public float GetFirerate() => Firerate;
        public int GetProjectileAmount() => ProjectileAmount;
        public float GetRange() => Range;
        public float GetSpeed() => Speed;
        public float GetSpread() => Spread;

        public bool TryFire()
        {
            if (CanFire() && _fireControl.All(x => x.TryFire()))
            {
                Fire();
                _chambered = false;
                StartCoroutine(Rechamber(Cooldown));
                return true;
            }
            return false;
        }

        public bool CanFire ()
            => _chambered;

        private void Fire()
        {
            _fireAnimation.Play(Cooldown);
            _fireSequence.Fire(_muzzles.Length, Cooldown, (i) =>
            {
                IProjectile[] projs = _projectilePool.Get(_muzzles[i].position, _muzzles[i].rotation, GetSpread(), GetProjectileAmount());

                foreach (IProjectile proj in projs)
                {
                    HandleProjectile(proj);
                }

                OnFire?.Invoke(projs);
                EmitFireParticle(i);
            });
        }

        private void HandleProjectile(IProjectile projectile)
        {
            Projectile proj = projectile as Projectile; // TODO: Extend this to an external ProjectileHandler class, to support other projectile implementations. If needed.
            proj.Speed = GetSpeed() * UnityEngine.Random.Range(0.9f, 1.1f);
            proj.Damage = GetDamage();
            proj.Range = GetRange() * UnityEngine.Random.Range(0.9f, 1.1f); ;
            proj.Pierce = Pierce;
            proj.Layer = HitLayer;
            proj.Color = Color;
            proj.Target = Target;
            proj.Pool = _pool;
            proj.Init();
        }

        private void EmitFireParticle(int index)
        {
            if (_fireParticles[index])
            {
                _fireParticles[index].Play();
            }
        }

        private void OnNewProjectile(IProjectile obj)
        {
            OnProjectile?.Invoke(obj);
            obj.OnHit += OnProjectileHit;
            obj.OnKill += OnProjectileKill;
            obj.OnDepleted += OnProjectileDepleted;
        }

        public void AddFireControl(IFireControl ctrl)
        {
            _fireControl.Add(ctrl);
        }

        public void RemoveFireControl(IFireControl ctrl)
        {
            _fireControl.Remove(ctrl);
        }

        public void RemoveFireControl(Predicate<IFireControl> predicate)
        {
            _fireControl.RemoveAll(predicate);
        }

        private Transform[] GetMuzzles()
        {
            List<Transform> muzzles = new List<Transform>();
            Transform muzzleParent = transform.Find(MUZZLE_PARENT_NAME);
            foreach (Transform child in muzzleParent)
            {
                muzzles.Add(child);
            }
            return muzzles.ToArray();
        }

        private ParticleSystem[] GetFireParticles(Transform[] muzzles)
        {
            ParticleSystem[] particles = new ParticleSystem[muzzles.Length];
            for (int i = 0; i < muzzles.Length; i++)
            {
                Transform child = muzzles[i].Find("FireParticle");
                if (child)
                {
                    particles[i] = child.GetComponent<ParticleSystem>();
                }
            }
            return particles;
        }
    }
}
