using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public interface IProjectilePool
    {
        IProjectile Get(Vector3 position, Quaternion rotation);
        event Action<IProjectile> OnProjectileInstantiated;

        void ClearObjectPool();
    }

    public static class ProjectilePoolExtensions
    {
        public static IProjectile[] Get(this IProjectilePool projPool, Vector3 position, Quaternion rotation, int amount)
        {
            IProjectile[] result = new IProjectile[amount];
            for (int i = 0; i < amount; i++)
            {
                result[i] = projPool.Get(position, rotation);
            }
            return result;
        }

        public static IProjectile Get(this IProjectilePool projPool, Vector3 position, Quaternion rotation, float deviation)
        {
            float d = UnityEngine.Random.Range(-deviation, deviation);
            Vector3 direction = rotation * new Vector3(1f, Mathf.Sin(Mathf.Deg2Rad * d), 0f);
            IProjectile proj = projPool.Get(position, rotation);
            (proj as Component).transform.right = direction;
            return proj;
        }

        public static IProjectile[] Get (this IProjectilePool projPool, Vector3 position, Quaternion rotation, float deviation, int amount)
        {
            IProjectile[] result = new IProjectile[amount];
            for (int i = 0; i < amount; i++)
            {
                result[i] = projPool.Get(position, rotation, deviation);
            }
            return result;
        }
    }
}