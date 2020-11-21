using Lomztein.BFA2.Collectables;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets;
using Lomztein.BFA2.Structures.Turrets.Targeters;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures
{
    public class Collector : TurretComponent
    {
        [ModelProperty]
        public float AngleTreshold;

        public Transform _beamStart;
        public LineRenderer _beam;

        private ITargetProvider _targetProvider;
        private ITargeter _targeter;

        private Transform _target;
        private ICollectable _collectable;
        private float _currentProgress;

        public override TurretComponentCategory Category => TurretComponentCategories.Misc;

        public override void End()
        {
        }

        public override void Init()
        {
            _targetProvider = GetComponentInParent<ITargetProvider>();
            _targeter = GetComponentInParent<ITargeter>();

            _beamStart = transform.Find("BeamStart");
            _beam = transform.Find("Beam").GetComponent<LineRenderer>();
        }

        private void OnSwitchTarget (Transform newTarget)
        {
            _target = newTarget;
            _currentProgress = 0f;
         
            if (_target)
            {
                _collectable = _target.GetComponent<ICollectable>();
            }
        }


        public override void Tick(float deltaTime)
        {
            Transform target = _targetProvider.GetTarget();
            if (target != _target)
            {
                OnSwitchTarget(target);
            }
            TickCollection(deltaTime);
        }

        private void TickCollection(float deltaTime)
        {
            if (_target && _targeter.GetDistance() < AngleTreshold)
            {
                UpdateBeam(true);
                _currentProgress += deltaTime / _collectable.CollectionTime;
                if (_currentProgress >= 1f)
                {
                    Collect();
                }
            }
            else
            {
                UpdateBeam(false);
            }
        }

        private void Collect()
        {
            _collectable.Collect();
            Destroy(_target.gameObject);
            OnSwitchTarget(null);
        }

        private void UpdateBeam(bool enable)
        {
            _beam.enabled = enable;
            if (_target && enable)
            {
                _beam.SetPosition(0, _beamStart.position);
                _beam.SetPosition(1, _target.position);
            }
        }
    }
}
