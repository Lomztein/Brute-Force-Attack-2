using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.TargetProviders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.BFA2.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter
    {
        [TurretComponent]
        public ITargetProvider TargetProvider;

        [ModelProperty]
        public float Speed;

        private float _angleToTarget = 180;

        public override void End()
        {
        }

        public float GetDistance()
        {
            return _angleToTarget;
        }

        public override void Init()
        {
        }

        public override void Tick(float deltaTime)
        {
            if (TargetProvider != null)
            {
                Transform target = TargetProvider.GetTargets().FirstOrDefault();
                if (target != null)
                {
                    Vector3 tpos = target.position;
                    Vector3 spos = transform.position;

                    float angle = Mathf.Rad2Deg * Mathf.Atan2(tpos.y - spos.y, tpos.x - spos.x);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), Speed * deltaTime);
                    _angleToTarget = Mathf.Abs (Mathf.DeltaAngle(angle, transform.eulerAngles.z));
                }
                else
                {
                    _angleToTarget = 180f;
                }
            }
            else
            {
                _angleToTarget = 180f;
            }
        }
    }
}