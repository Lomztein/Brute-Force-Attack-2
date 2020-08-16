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
        private Projectile _parent;

        public void End()
        {
        }

        public void Init(Projectile parent)
        {
            _initialSpeed = parent.Speed;
            _parent = parent;
        }

        public void Link(IProjectilePool weapon)
        {
        }

        public void Tick(float deltaTime)
        {
            float max = _initialSpeed * MaxAccelerationMult;

            if (_parent.Speed < max)
            {
                _parent.Speed += _initialSpeed * AccelerationFactor * deltaTime;
            }
            else
            {
                _parent.Speed = max;
            }
        }
    }
}
