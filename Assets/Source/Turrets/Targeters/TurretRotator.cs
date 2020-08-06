using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.TargetProviders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.BFA2.Turrets.Weapons;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.UI.Tooltip;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Utilities;

namespace Lomztein.BFA2.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter
    {
        public ITargetProvider TargetProvider;
        private IWeapon _weapon;
        private float _startingAngle;

        public IStatReference Turnrate;

        private float _angleToTarget = 180;
        private Vector3? _prevPos;
        private Vector3 _tpos;
        public string Text => "Turnrate: " + Turnrate.GetValue();

        LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        public override void End()
        {
        }

        public float GetDistance()
        {
            return _angleToTarget;
        }

        public override void Init()
        {
            TargetProvider = GetComponentInParent<ITargetProvider>();
            AddAttribute(Modification.ModdableAttribute.Rotator);
            _weapon = GetComponentInChildren<IWeapon>();
            Turnrate = Stats.AddStat("Turnrate", "Rotation Speed", "The speed of which this rotator rotates.");
            _startingAngle = transform.eulerAngles.z;
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
                    _angleToTarget = Mathf.Abs(Mathf.DeltaAngle(angle, transform.eulerAngles.z));

                    _prevPos = target.position;
                    _tpos = tpos;
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, _startingAngle), Turnrate.GetValue() * deltaTime);
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
    }
}