using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.TargetProviders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.BFA2.Turrets.Weapons;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.UI.Tooltip;

namespace Lomztein.BFA2.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter
    {
        [TurretComponent]
        public ITargetProvider TargetProvider;
        private IWeapon _weapon;

        public IStatReference Turnrate;

        private float _angleToTarget = 180;
        private Vector3? _prevPos;
        private Vector3 _tpos;
        public string Text => "Turnrate: " + Turnrate.GetValue();

        public override void End()
        {
        }

        public float GetDistance()
        {
            return _angleToTarget;
        }

        public override void Init()
        {
            AddAttribute(Modification.ModdableAttribute.Rotator);
            _weapon = GetComponentInChildren<IWeapon>();
        }

        public override void Tick(float deltaTime)
        {
            if (TargetProvider != null)
            {
                Transform target = TargetProvider.GetTarget();
                if (target != null)
                {
                    if (!_prevPos.HasValue)
                    {
                        _prevPos = target.position;
                    }

                    Vector3 tpos = target.position;
                    Vector3 spos = transform.position;
                    Vector3 delta = (tpos - _prevPos ?? tpos) / deltaTime;

                    float time = 0f;
                    if (_weapon != null)
                    {
                        float dist = (spos - tpos).magnitude;
                        time = dist / _weapon.GetSpeed();
                    }

                    tpos += delta * time;
                    float angle = Mathf.Rad2Deg * Mathf.Atan2(tpos.y - spos.y, tpos.x - spos.x);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, angle), Turnrate.GetValue() * deltaTime);
                    _angleToTarget = Mathf.Abs (Mathf.DeltaAngle(angle, transform.eulerAngles.z));

                    _prevPos = target.position;
                    _tpos = tpos;
                }
                else
                {
                    _angleToTarget = 180f;
                    _prevPos = null;
                }
            }
            else
            {
                _angleToTarget = 180f;
                _prevPos = null;
            }
        }

        private void OnDrawGizmos()
        {
            if (TargetProvider?.GetTarget() != null)
            {
                Gizmos.DrawLine(transform.position, _tpos);
                Gizmos.DrawSphere(_tpos, 0.5f);
                if (_prevPos.HasValue)
                {
                    Gizmos.DrawWireSphere(_prevPos.Value, 0.75f);
                }
            }
        }

        public override void InitStats()
        {
            Turnrate = Stats.AddStat("Turnrate", "Rotation Speed", "The speed of which this rotator rotates.");
        }

        public override void InitEvents()
        {
        }
    }
}