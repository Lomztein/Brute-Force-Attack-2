using Lomztein.BFA2.Weaponary.Projectiles;
using Lomztein.BFA2.Weaponary.Targeting;
using System;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public interface IWeapon
    {
        event Action<IProjectile[]> OnFire;
        event Action<IProjectile> OnProjectile;
        event Action<HitInfo> OnProjectileDepleted;
        event Action<HitInfo> OnProjectileHit;
        event Action<HitInfo> OnProjectileKill;

        float Damage { get; set; }
        float Firerate { get; set; }
        int ProjectileAmount { get; set; }
        float Speed { get; set; }
        float Spread { get; set; }
        float Range { get; set; }
        float Pierce { get; set; }
        Colorization.Color Color { get; set; }
        int MuzzleCount { get; }

        void Init();
        bool TryFire(ITarget target, object source);
        bool CanFire();

    }
}