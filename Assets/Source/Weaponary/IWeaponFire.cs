using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public interface IWeaponFire
    {
        IProjectile[] Fire(Vector3 position, Quaternion rotation, IProjectileInfo info, float speed, float deviation, int amount);
        void ClearObjectPool();

        event Action<HitInfo> OnHit;
        event Action<HitInfo> OnKill;
    }
}