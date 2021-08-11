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
        Projectile _parent;
        float _baseSpeed;

        [ModelProperty]
        public Vector2 RotateMinMax;
        public AnimationCurve RotateCurve;

        private TargetFinder _retargeter;
        private bool Retarget => _retargeter != null;

        public void End()
        {
        }

        private void Awake()
        {
            _retargeter = GetComponent<TargetFinder>();
        }

        public void Init(Projectile parent)
        {
            _parent = parent;
            _baseSpeed = parent.Speed;
        }

        public void Link(IProjectilePool weapon)
        {
        }

        public void Tick(float deltaTime)
        {
            if (_parent != null && _parent.Target != null)
            {
                float angle = Mathf.Atan2(_parent.Target.position.y - transform.position.y, _parent.Target.position.x - transform.position.x) * Mathf.Rad2Deg;
                float dot = Vector3.Dot(transform.right, Vector3.Normalize(_parent.Target.position - transform.position));

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), GetRotSpeed(dot) * deltaTime);
            }
            if (Retarget && _parent != null && _parent.Target == null)
            {
                _parent.Target = _retargeter.FindTarget(gameObject, Physics2D.OverlapCircleAll(transform.position, _parent.Range, _parent.Layer));
            }
        }

        private float GetRotSpeed (float dot)
        {
            return MapDot(-dot, RotateMinMax, RotateCurve);
        }

        private float MapDot (float dot, Vector2 minmax, AnimationCurve curve)
        {
            float t = (dot + 1) / 2f;
            return Mathf.Lerp(minmax.x, minmax.y, curve.Evaluate (t));
        }
    }
}
