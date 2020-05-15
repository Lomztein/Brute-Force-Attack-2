using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Pooling;
using Lomztein.BFA2.Weaponary.FireControl;
using Lomztein.BFA2.Weaponary.Projectiles;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary
{
    // TODO: Find an actual use for this class, or remove it completely.
    public class WeaponFire : MonoBehaviour, IWeaponFire
    {
        public event Action<HitInfo> OnHit;
        public event Action<HitInfo> OnKill;

        private IProjectileInstantiator _projectileInstantiator;

        public void OnAssembled()
        {
            _projectileInstantiator = GetComponent<IProjectileInstantiator>();
        }

        public void Awake()
        {
            _projectileInstantiator = GetComponent<IProjectileInstantiator>();
        }

        private void Start()
        {
            _projectileInstantiator.Source.OnNew += OnNewFromPool;
        }

        private void OnNewFromPool(IProjectile obj)
        {
            obj.OnHit += CallOnHit;
            obj.OnKill += CallOnKill;
        }

        private void CallOnHit(HitInfo info) => OnHit?.Invoke(info);
        private void CallOnKill(HitInfo info) => OnKill?.Invoke(info);

        public void ClearObjectPool()
        {
            _projectileInstantiator.Source.Clear();
        }

        public IProjectile[] Fire(Vector3 position, Quaternion rotation, IProjectileInfo info, float speed, float deviation, int amount)
        {
            return _projectileInstantiator.Create(info, position, rotation, amount, deviation, speed);
        }
    }
}
