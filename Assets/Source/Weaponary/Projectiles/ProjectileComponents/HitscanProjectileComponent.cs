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
        private IWeaponFire _weapon;
        private IProjectile _parent;
        [ModelProperty]
        public float Life;

        private void Start()
        {
            _renderer = GetComponentInChildren<IHitscanRenderer>();
        }

        public void End()
        {
        }


        public void Init(IProjectile parent)
        {
            Ray ray = new Ray(transform.position, parent.Info.Direction);
            RaycastHit[] hits = Physics.RaycastAll(ray, parent.Info.Range, parent.Info.Layer);
            foreach (RaycastHit hit in hits)
            {
                IDamagable damagable = parent.CheckHit(hit.collider);
                if (damagable != null)
                {
                    parent.Info.Damage -= parent.Hit(damagable, new HitInfo (hit.collider, hit.point, parent, _weapon));
                    if (parent.Info.Damage <= 0f)
                    {
                        _renderer.SetPositions(transform.position, hit.point);
                        break;
                    }
                }
            }

            if (parent.Info.Damage > 0f)
            {
                _renderer.SetPositions(transform.position, ray.GetPoint(parent.Info.Range));
            }
            Invoke("FuckingDie", Life);
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
        }
    }
}
