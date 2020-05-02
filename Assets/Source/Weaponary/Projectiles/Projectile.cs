using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    class Projectile : MonoBehaviour, IProjectile
    {
        public IProjectileInfo Info { get; set; }

        public bool Ready => !gameObject.activeSelf;

        private List<IProjectileComponent> _projectileComponents = new List<IProjectileComponent>();

        public event Action<HitInfo> OnHit;
        public event Action<HitInfo> OnKill;

        public void Init()
        {
            transform.position = Info.Position;
            _projectileComponents.AddRange(GetComponents<IProjectileComponent>());
            _projectileComponents.ForEach(x => x.Init(this));
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
            _projectileComponents.ForEach(x => x.Link(weapon));
        }

        public IDamagable CheckHit (Collider hit)
        {
            IDamagable damagable = hit.GetComponent<IDamagable>();
            if (damagable != null)
            {
                return damagable;
            }
            return null;
        }

        public float Hit (IDamagable damagable, HitInfo info)
        {
            DamageInfo damage = new DamageInfo(Info.Damage, Info.Color);
            float life = damagable.TakeDamage(damage);
            OnHit?.Invoke(info);
            if (life > 0f)
            {
                OnKill?.Invoke(info);
            }
            return life;
        }

        public void End ()
        {
            Info.Pool.Insert(this);
            _projectileComponents.ForEach(x => x?.End());
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
        }
    }
}
