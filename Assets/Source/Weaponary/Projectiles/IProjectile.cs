using Lomztein.BFA2.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public interface IProjectile : IPoolObject
    {
        void Init();
        void Deplete();
        void End();

        void Link(IProjectilePool weapon);

        IDamagable CheckHit(Collider2D hit);
        DamageInfo Hit(IDamagable damagable, Collider2D col, Vector3 position, Vector3 normal);

        event Action<HitInfo> OnDepleted;
        event Action<HitInfo> OnHit;
        event Action<HitInfo> OnKill;
    }
}