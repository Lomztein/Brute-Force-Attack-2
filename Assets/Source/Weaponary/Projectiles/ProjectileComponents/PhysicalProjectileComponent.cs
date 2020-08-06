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
        private IProjectile _parent;
        private IWeaponFire _weapon;

        public void End()
        {
        }

        public void Init(IProjectile parent)
        {
            _parent = parent;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(_parent.Info.Direction.y, _parent.Info.Direction.x) * Mathf.Rad2Deg);
            Invoke("FuckingDie", parent.Info.Range / _parent.Info.Speed);
        }

        private void FuckingDie ()
        {
            _parent.End();
        }

        public void Link(IWeaponFire weapon)
        {
            _weapon = weapon;
        }

        public void Tick(float deltaTime)
        {
            Ray2D ray = new Ray2D(transform.position, _parent.Info.Direction * _parent.Info.Speed * deltaTime);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, _parent.Info.Speed * deltaTime, _parent.Info.Layer);
            foreach (RaycastHit2D hit in hits)
            {
                IDamagable damagable = _parent.CheckHit(hit.collider);
                if (damagable != null)
                {
                    DamageInfo damageInfo = _parent.Hit(damagable, hit.collider, hit.point, hit.normal);
                    _parent.Info.Damage -= damageInfo.DamageDealt;
                    if (_parent.Info.Damage <= 0f)
                    {
                        _parent.End();
                        break;
                    }
                }
            }
            transform.position += _parent.Info.Direction * _parent.Info.Speed * deltaTime;
        }
    }
}
