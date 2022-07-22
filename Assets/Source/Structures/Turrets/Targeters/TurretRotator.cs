using Lomztein.BFA2.Serialization;
using UnityEngine;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.UI.ToolTip;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Utilities;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Weaponary;

namespace Lomztein.BFA2.Structures.Turrets.Targeters
{
    public class TurretRotator : TurretComponent, ITargeter, IRotator
    {
        public ITargetProvider TargetProvider;
        private IWeapon _weapon;
        private float _startingAngle;

        [ModelAssetReference]
        public StatInfo TurnrateInfo;
        [ModelProperty]
        public float BaseTurnrate;
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

        public override void PreInit()
        {
            _weapon = GetComponentInChildren<IWeapon>();
            AddTag("Rotator");
            Turnrate = Stats.AddStat(TurnrateInfo, BaseTurnrate, this);
        }

        public override void Init()
        {
            TargetProvider = GetComponentInParent<ITargetProvider>();
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
                        time = dist / _weapon.Speed;
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

        public float GetAngle()
        {
            return transform.rotation.eulerAngles.z;
        }
    }
}