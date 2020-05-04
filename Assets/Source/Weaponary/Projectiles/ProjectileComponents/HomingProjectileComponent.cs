using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.Projectiles.ProjectileComponents
{
    public class HomingProjectileComponent : MonoBehaviour, IProjectileComponent
    {
        IProjectile _parent;
        float _baseSpeed;
        [ModelProperty]
        public Vector2 RotateMinMax;
        [ModelProperty]
        public Vector2 SpeedMultMinMax;
        private ITargetFinder _retargeter;
        private bool Retarget => _retargeter != null;

        public void End()
        {
        }

        private void Awake()
        {
            _retargeter = GetComponent<ITargetFinder>();
        }

        public void Init(IProjectile parent)
        {
            _parent = parent;
            _baseSpeed = parent.Info.Speed;
        }

        public void Link(IWeaponFire weapon)
        {
        }

        public void Tick(float deltaTime)
        {
            if (_parent?.Info?.Target != null)
            {
                float angle = Mathf.Atan2(_parent.Info.Target.position.y - transform.position.y, _parent.Info.Target.position.x - transform.position.x) * Mathf.Rad2Deg;
                float dot = Vector3.Dot(transform.right, (_parent.Info.Target.position - transform.position).normalized);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), GetRotSpeed(dot) * deltaTime);
                _parent.Info.Speed = GetSpeed(dot);
                _parent.Info.Direction = transform.right;
            }
            if (Retarget && _parent?.Info?.Target == null)
            {
                _parent.Info.Target = _retargeter.FindTarget(Physics2D.OverlapCircleAll(transform.position, _parent.Info.Range, _parent.Info.Layer));
            }
        }

        private float GetRotSpeed (float dot)
        {
            return MapDot(-dot, RotateMinMax);
        }

        private float GetSpeed (float dot)
        {
            return _baseSpeed * MapDot(dot, SpeedMultMinMax);
        }

        private float MapDot (float dot, Vector2 minmax)
        {
            float t = (dot + 1) / 2f;
            return Mathf.Lerp(minmax.x, minmax.y, t);
        }
    }
}
