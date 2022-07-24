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
using UnityEngine.Assertions;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile, IColored
    {
        public bool Ready => !gameObject.activeSelf && !AreEffectsActive();

        [ModelProperty]
        public float Speed;
        [ModelProperty]
        public double Damage;
        [ModelProperty]
        public float Range;
        [ModelProperty, Range(0f, 1f)]
        public float Pierce;
        [ModelProperty]
        public LayerMask Layer;
        [ModelProperty]
        public Colorization.Color Color;
        public Transform Target;
        public IObjectPool<IProjectile> Pool;

        private List<IProjectileComponent> _projectileComponents = new List<IProjectileComponent>();
        private IProjectilePool _weapon;

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
        public event Action<HitInfo> OnDepleted;

        public void Init()
        {
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

        public float GetPierceFactor ()
        {
            Assert.IsTrue(Pierce >= 0f && Pierce <= 1f, "Pierce must be between 0 and 1.");
            return 1 - Pierce;
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

        public void FixedUpdate()
        {
            _projectileComponents.ForEach(x => x.Tick(Time.fixedDeltaTime));
        }

        public void Link(IProjectilePool weapon)
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
            DamageInfo damage = new DamageInfo(Damage, Color);
            double life = damagable.TakeDamage(damage);

            HitInfo info = new HitInfo(damage, col, position, normal, this, _weapon, Damage <= 0f);

            if (Damage - damage.DamageDealt <= 0f)
            {
                EmitHitEffect(position, normal);
                OnDepleted?.Invoke(info);
            }

            OnHit?.Invoke(info);
            if (life <= 0.001f)
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
            Pool.Insert(this);
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

        public void Deplete ()
        {
            Damage = 0f;
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
            return Color;
        }
    }
}
