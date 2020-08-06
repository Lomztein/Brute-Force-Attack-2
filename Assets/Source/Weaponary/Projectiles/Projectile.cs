using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    class Projectile : MonoBehaviour, IProjectile, IColorProvider
    {
        public IProjectileInfo Info { get; set; }

        public bool Ready => !gameObject.activeSelf && !AreEffectsActive();

        private List<IProjectileComponent> _projectileComponents = new List<IProjectileComponent>();
        private IWeaponFire _weapon;

        private GameObjectActiveToggle _hitEffectObj;
        private GameObjectActiveToggle _trailEffectObj;
        private Vector3 _trailEffectLocalPosition = Vector3.zero;
        private Quaternion _trailEffectLocalRotation = Quaternion.identity;

        // TODO: Expand to an Effect system that supports both particle systems and any other types of effect.
        private ParticleSystem _hitEffect;
        private ParticleSystem _trailEffect;

        [ModelProperty]
        public float HitEffectLife;
        [ModelProperty]
        public float TrailEffectLife;

        public event Action<HitInfo> OnHit;
        public event Action<HitInfo> OnKill;

        public void Init()
        {
            transform.position = Info.Position;
            _projectileComponents.ForEach(x => x.Init(this));

            if (_hitEffectObj)
            {
                _hitEffectObj.transform.parent = transform;
                _hitEffectObj.Activate();
            }

            if (_trailEffectObj)
            {
                _trailEffectObj.transform.parent = transform;
                _trailEffectObj.transform.localPosition = _trailEffectLocalPosition;
                _trailEffectObj.transform.localRotation = _trailEffectLocalRotation;
                _trailEffectObj.Activate();
            }
        }

        public void Awake()
        {
            _projectileComponents.AddRange(GetComponents<IProjectileComponent>());

            Transform hitEffect = transform.Find("HitEffect");
            Transform trailEffect = transform.Find("TrailEffect");

            if (hitEffect)
            {
                _hitEffectObj = hitEffect.GetComponent<GameObjectActiveToggle>();
                _hitEffect = hitEffect.GetComponent<ParticleSystem>();
            }
            if (trailEffect)
            {
                _trailEffectObj = trailEffect.GetComponent<GameObjectActiveToggle>();
                if (_trailEffectObj)
                {
                    _trailEffect = _trailEffectObj.GetComponent<ParticleSystem>();
                    _trailEffectLocalPosition = _trailEffectObj.transform.localPosition;
                    _trailEffectLocalRotation = _trailEffectObj.transform.localRotation;
                }
            }
        }

        public void AddProjectileComponent (IProjectileComponent component)
        {
            _projectileComponents.Add(component);
        }

        public void RemoveProjectileComponent (IProjectileComponent component)
        {
            _projectileComponents.Remove(component);
        }

        public void FixedUpdate()
        {
            _projectileComponents.ForEach(x => x.Tick(Time.fixedDeltaTime));
        }

        public void Link(IWeaponFire weapon)
        {
            _weapon = weapon;
        }

        public IDamagable CheckHit (Collider2D hit)
        {
            IDamagable damagable = hit.GetComponent<IDamagable>();
            if (damagable != null)
            {
                return damagable;
            }
            return null;
        }

        public DamageInfo Hit (IDamagable damagable, Collider2D col, Vector3 position, Vector3 normal)
        {
            DamageInfo damage = new DamageInfo(Info.Damage, Info.Color);
            float life = damagable.TakeDamage(damage);

            HitInfo info = new HitInfo(damage, col, position, normal, this, _weapon, Info.Damage <= 0f);

            if (Info.Damage - damage.DamageDealt <= 0f)
            {
                EmitHitEffect(position, normal);
            }

            OnHit?.Invoke(info);
            if (life > 0f)
            {
                OnKill?.Invoke(info);
            }
            return damage;
        }

        public void EmitHitEffect(Vector3 position, Vector3 normal)
        {
            if (_hitEffect)
            {
                _hitEffect.transform.parent = null;
                _hitEffect.transform.position = position;
                _hitEffect.transform.rotation = Quaternion.Euler(Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 180f, -90, 0f);
                _hitEffect.Play();
            }
        }

        public void End ()
        {
            Info.Pool.Insert(this);
            _projectileComponents.ForEach(x => x?.End());

            if (_hitEffectObj)
            {
                _hitEffectObj.transform.parent = null;
                _hitEffectObj.DelayedDeactivate(HitEffectLife);
            }

            if (_trailEffectObj)
            {
                _trailEffectObj.transform.parent = null;
                _trailEffectObj.DelayedDeactivate(TrailEffectLife);
                _trailEffect.Stop();
            }
        }

        public void DisableSelf()
        {
            gameObject.SetActive(false);
        }

        public void EnableSelf()
        {
            gameObject.SetActive(true);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
            if (_hitEffectObj)
            {
                Destroy(_hitEffectObj.gameObject, HitEffectLife);
            }
            if (_trailEffectObj)
            {
                Destroy(_trailEffectObj.gameObject, TrailEffectLife);
            }
        }

        private bool AreEffectsActive ()
        {
            if (_hitEffectObj)
            {
                if (!_hitEffectObj.gameObject.activeSelf)
                {
                    return false;
                }
            }
            if (_trailEffectObj)
            {
                if (!_trailEffectObj.gameObject.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }

        public Colorization.Color GetColor()
        {
            return Info?.Color ?? Colorization.Color.Blue;
        }
    }
}
