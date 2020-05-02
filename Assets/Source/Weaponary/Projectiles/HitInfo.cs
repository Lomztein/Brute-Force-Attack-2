using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public class HitInfo
    {
        public HitInfo (Collider collider, Vector3 point, IProjectile projectile, IWeaponFire weapon)
        {
            Collider = collider;
            Point = point;
            Projectile = projectile;
            Weapon = weapon;
        }

        public Collider Collider;
        public Vector3 Point;
        public IProjectile Projectile;
        public IWeaponFire Weapon;
    }
}
