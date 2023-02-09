using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents.HitscanRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents
{
    public class HitscanProjectileComponent : MonoBehaviour, IProjectileComponent
    {
        private IHitscanRenderer _renderer;
        private Projectile _parent;
        [ModelProperty]
        public float Life;

        private void Awake()
        {
            _renderer = GetComponentInChildren<IHitscanRenderer>();
        }

        public void End()
        {
        }

        public void Init(Projectile parent)
        {
            _parent = parent;
            Ray2D ray = new Ray2D(transform.position, transform.right);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, parent.Range, parent.Layer);
            foreach (RaycastHit2D hit in hits)
            {
                IDamagable damagable = parent.CheckHit(hit.collider);
                if (damagable != null)
                {
                    HitInfo hitInfo = parent.Hit(damagable, hit.collider, hit.point, hit.normal);
                    _parent.Damage -= hitInfo.DamageInfo.DamageDealt * _parent.GetPierceFactor();
                    if (parent.Damage <= 1f)
                    {
                        _renderer.SetPositions(transform.position, hit.point);
                        break;
                    }
                }
            }

            if (parent.Damage > 0f)
            {
                _renderer.SetPositions(transform.position, ray.GetPoint(parent.Range));
            }
            Invoke("FuckingDie", Life);
        }

        private void FuckingDie ()
        {
            _parent.End();
        }

        public void Tick(float deltaTime)
        {
        }
    }
}
