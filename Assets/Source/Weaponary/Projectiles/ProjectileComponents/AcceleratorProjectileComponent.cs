using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents
{
    public class AcceleratorProjectileComponent : MonoBehaviour, IProjectileComponent
    {
        public float AccelerationFactor;
        public float MaxAccelerationMult;

        private float _initialSpeed;
        private IProjectile _parent;

        public void End()
        {
        }

        public void Init(IProjectile parent)
        {
            _initialSpeed = parent.Info.Speed;
            _parent = parent;
        }

        public void Link(IWeaponFire weapon)
        {
        }

        public void Tick(float deltaTime)
        {
            float max = _initialSpeed * MaxAccelerationMult;

            if (_parent.Info.Speed < max)
            {
                _parent.Info.Speed += _initialSpeed * AccelerationFactor * deltaTime;
            }
            else
            {
                _parent.Info.Speed = max;
            }
        }
    }
}
