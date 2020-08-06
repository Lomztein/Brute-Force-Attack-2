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
        public HitInfo (DamageInfo damage, Collider2D collider, Vector3 point, Vector3 normal, IProjectile projectile, IWeaponFire weapon, bool final)
        {
            Damage = damage;
            Collider = collider;
            Point = point;
            Normal = normal;
            Projectile = projectile;
            Weapon = weapon;
            Final = final;
        }

        public DamageInfo Damage;
        public Collider2D Collider;
        public Vector3 Point;
        public Vector3 Normal;
        public IProjectile Projectile;
        public IWeaponFire Weapon;
        public bool Final;
    }
}
