using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents
{
    public class PhysicalProjectileComponent : MonoBehaviour, IProjectileComponent
    {
        private Projectile _parent;

        public void End()
        {
        }

        public void Init(Projectile parent)
        {
            _parent = parent;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(_parent.transform.right.y, _parent.transform.right.x) * Mathf.Rad2Deg);
            Invoke("FuckingDie", parent.Range / _parent.Speed);
        }

        private void FuckingDie ()
        {
            _parent.End();
        }

        public void Tick(float deltaTime)
        {
            Ray2D ray = new Ray2D(transform.position, _parent.transform.right * _parent.Speed * deltaTime);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, _parent.Speed * deltaTime, _parent.Layer);
            foreach (RaycastHit2D hit in hits)
            {
                IDamagable damagable = _parent.CheckHit(hit.collider);
                if (damagable != null)
                {
                    DamageInfo damageInfo = _parent.Hit(damagable, hit.collider, hit.point, hit.normal);
                    _parent.Damage -= damageInfo.DamageDealt;
                    if (_parent.Damage <= 0f)
                    {
                        _parent.End();
                        break;
                    }
                }
            }
            transform.position += _parent.transform.right * _parent.Speed * deltaTime;
        }
    }
}
