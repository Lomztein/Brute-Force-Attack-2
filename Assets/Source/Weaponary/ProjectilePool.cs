using System;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Weaponary.Projectiles;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    public class ProjectilePool : IProjectilePool
    {
        private IObjectPool<IProjectile> _pool;

        public event Action<IProjectile> OnProjectileInstantiated;

        public ProjectilePool(IObjectPool<IProjectile> pool)
        {
            _pool = pool;
            _pool.OnNew += OnNew;
        }

        private void OnNew(IProjectile obj)
        {
            OnProjectileInstantiated?.Invoke(obj);
        }

        public void ClearObjectPool()
        {
            _pool.Clear();
        }

        public IProjectile Get(Vector3 position, Quaternion rotation)
        {
            IProjectile proj = _pool.Get();
            Component comp = proj as Component;

            comp.transform.position = position;
            comp.transform.rotation = rotation;

            return proj;
        }
    }
}
