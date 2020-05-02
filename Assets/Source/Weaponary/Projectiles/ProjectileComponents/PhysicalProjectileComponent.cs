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
            Ray ray = new Ray(transform.position, _parent.Info.Direction * _parent.Info.Speed * deltaTime);
            RaycastHit[] hits = Physics.RaycastAll(ray, _parent.Info.Speed * deltaTime, _parent.Info.Layer);
            foreach (RaycastHit hit in hits)
            {
                IDamagable damagable = _parent.CheckHit(hit.collider);
                if (damagable != null)
                {
                    _parent.Info.Damage -= _parent.Hit(damagable, new HitInfo(hit.collider, hit.point, _parent, _weapon));
                    if (_parent.Info.Damage <= 0f)
                    {
                        break;
                    }
                }
            }
            transform.position += _parent.Info.Direction * _parent.Info.Speed * deltaTime;
        }
    }
}
