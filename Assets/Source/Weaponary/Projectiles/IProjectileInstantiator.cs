using Lomztein.BFA2.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles
{
    public interface IProjectileInstantiator
    {
        IObjectPool<IProjectile> Source { get; set; }

        IProjectile[] Create(IProjectileInfo info, Vector3 position, Quaternion rotation, int amount, float deviation, float speed);
    }
}