using Lomztein.BFA2.Animation.FireAnimations;
using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Attachment;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using Lomztein.BFA2.Structures.Turrets.Targeters;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireControl;
using Lomztein.BFA2.Weaponary.FireSynchronization;
using Lomztein.BFA2.Weaponary.Projectiles;
using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Structures.Turrets.Weapons
{
    public class TurretWeapon : TurretComponent, IColorProvider, IWeapon {

        public ITargeter Targeter;
        public ITargetProvider Provider;
        public IRanger Ranger;

        [ModelProperty]
        public ContentPrefabReference ProjectilePrefab;
        [ModelProperty]
        public float FireTreshold;
        [ModelProperty]
        public LayerMask HitLayer;

        [ModelProperty]
        public float BaseDamage;
        [ModelProperty]
        public int BaseProjectileAmount;
        [ModelProperty]
        public float BaseSpread;
        [ModelProperty]
        public float BaseSpeed;
        [ModelProperty]
        public float BaseFirerate;

        private Transform[] _muzzles;
        private const string MUZZLE_PARENT = "Muzzles";
        public IStatReference Damage;
        public IStatReference ProjectileAmount;
        public IStatReference Spread;
        public IStatReference Speed;
        public IStatReference Firerate;

        public IEventCaller<HitEventArgs> OnHit;
        public IEventCaller<HitEventArgs> OnKill;

        public float Cooldown => 1f / Firerate.GetValue();

        public override StructureCategory Category => StructureCategories.Weapon;

        private IObjectPool<IProjectile> _pool;
        private IProjectilePool _projectilePool;
        private IFireAnimation _fireAnimation;
        private IFireControl _fireControl;
        private IFireSynchronization _fireSync;

        [ModelProperty]
        public Color Color;
        private bool _chambered;

        private ParticleSystem[] _fireParticles;

        public override void End()
        {
        }

        public float GetDamage() => Damage.GetValue();

        public float GetFirerate() => Firerate.GetValue();

        public int GetProjectileAmount() => (int)ProjectileAmount.GetValue();
        public int GetMuzzleAmount() => _muzzles == null ? 0 : _muzzles.Length;

        public float GetSpread() => Spread.GetValue();

        public float GetSpeed() => Speed?.GetValue() ?? 1337f;

        private Transform[] GetMuzzles ()
        {
            List<Transform> muzzles = new List<Transform>();
            Transform muzzleParent = transform.Find(MUZZLE_PARENT);
            foreach (Transform child in muzzleParent)
            {
                muzzles.Add(child);
            }
            return muzzles.ToArray();
        }

        private ParticleSystem[] GetFireParticles (Transform[] muzzles)
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

        public override void PreInit()
        {
            UpperAttachmentPoints = new EmptyAttachmentPointSet();

            _fireAnimation = GetComponent<IFireAnimation>() ?? new NoFireAnimation();
            _fireControl = GetComponent<IFireControl>() ?? new NoFireControl();
            _fireSync = GetComponent<IFireSynchronization>() ?? new NoFireSynchronization();

            _pool = new NoGameObjectPool<IProjectile>(ProjectilePrefab);
            _projectilePool = new ProjectilePool(_pool);
            _pool.OnNew += OnNewProjectile;

            Damage = Stats.AddStat("Damage", "Damage", "The damage each projectile does.", BaseDamage);
            ProjectileAmount = Stats.AddStat("ProjectileAmount", "Projectile Amount", "How many projectiles are fired at once.", BaseProjectileAmount);
            Spread = Stats.AddStat("Spread", "Spread", "How much the projectiles spread.", BaseSpread);
            Speed = Stats.AddStat("Speed", "Speed", "How fast the projectiles fly.", BaseSpeed);
            Firerate = Stats.AddStat("Firerate", "Firerate", "How quickly the weapon fires.", BaseFirerate);

            OnHit = Events.AddEvent<HitEventArgs>("OnHit", "On Hit", "Executed when this weapon hits something.");
            OnKill = Events.AddEvent<HitEventArgs>("OnKill", "On Kill", "Executed when this weapon kills something.");
        }

        public override void Init()
        {
            _muzzles = GetMuzzles();
            _fireParticles = GetFireParticles(_muzzles);

            Targeter = GetComponentInParent<ITargeter>();
            Provider = GetComponentInParent<ITargetProvider>();
            Ranger = GetComponentInParent<IRanger>();

            AddModdableAttribute(Modification.ModdableAttribute.Weapon);

            if (isActiveAndEnabled)
            {
                _chambered = false;
                StartCoroutine(Rechamber(Cooldown));
            }
        }

        public void Synchronize(IFireSynchronization sync)
        {
            _fireSync = sync;
        }

        private void OnNewProjectile(IProjectile obj)
        {
            obj.OnHit += OnProjectileHit;
            obj.OnKill += OnProjectileKill;
        }

        private void OnProjectileKill(HitInfo obj)
        {
            OnKill.CallEvent(new HitEventArgs(obj));
        }

        private void OnProjectileHit(HitInfo obj)
        {
            OnHit.CallEvent(new HitEventArgs(obj));
        }

        private IEnumerator Rechamber(float cooldown)
        {
            yield return UnityUtils.WaitForFixedSeconds(cooldown);
            _chambered = true;
        }

        public override void Tick(float deltaTime)
        {
            if (Targeter != null && Targeter.GetDistance() < FireTreshold)
            {
                TryFire();
            }
        }

        public bool TryFire()
        {
            if (_chambered && _fireSync.TryFire())
            {
                Fire();
                _chambered = false;
                StartCoroutine(Rechamber(Cooldown));
                return true;
            }
            return false;
        }

        private void Fire()
        {
            _fireAnimation.Play(Cooldown);
            _fireControl.Fire(_muzzles.Length, Cooldown, (i) =>
            {
                IProjectile[] projs = _projectilePool.Get(_muzzles[i].position, _muzzles[i].rotation, Spread.GetValue(), (int)ProjectileAmount.GetValue());

                foreach (IProjectile proj in projs)
                {
                    HandleProjectile(proj);
                }

                EmitFireParticle(i);
            });
        }

        private void HandleProjectile(IProjectile projectile)
        {
            Projectile proj = projectile as Projectile; // TODO: Extend this to an external ProjectileHandler class, to support other projectile implementations. If needed.
            proj.Speed = GetSpeed() * Random.Range(0.9f, 1.1f);
            proj.Damage = GetDamage();
            proj.Range = GetRange() * Random.Range(0.9f, 1.1f); ;
            proj.Layer = HitLayer;
            proj.Color = GetColor();
            proj.Target = Provider.GetTarget();
            proj.Pool = _pool;
            proj.Init();
        }

        private void EmitFireParticle (int index)
        {
            if (_fireParticles[index])
            {
                _fireParticles[index].Play();
            }
        }

        public Color GetColor()
        {
            return Color;
        }
        public float GetRange()
        {
            if (Ranger != null)
            {
                return Ranger.GetRange();
            }
            else
            {
                return 50;
            }
        }
    }
}