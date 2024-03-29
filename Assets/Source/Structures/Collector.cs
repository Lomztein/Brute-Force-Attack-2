﻿using Lomztein.BFA2.Collectables;
using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
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
        private ParticleSystemForceField _particleForceField;

        private ITargetProvider _targetProvider;
        private ITargeter _targeter;

        private Transform _target;
        private Collectable _collectable;

        [ModelAssetReference]
        public EventInfo OnCollectedInfo;
        public IEventCaller OnCollected;

        [ModelAssetReference]
        public StatInfo CollectionRateInfo;
        public float BaseCollectionRate = 1f;
        private IStatReference _collectionRate;

        public override void End()
        {
        }

        public override void PreInit()
        {
            base.PreInit();
            OnCollected = Events.AddEvent(OnCollectedInfo, this);
            _collectionRate = Stats.AddStat(CollectionRateInfo, BaseCollectionRate, this);
        }

        public override void Init()
        {
            _targetProvider = GetComponentInParent<ITargetProvider>();
            _targeter = GetComponentInParent<ITargeter>();

            _beamStart = transform.Find("BeamStart");
            _beam = transform.Find("Beam").GetComponent<LineRenderer>();
            _particleForceField = GetComponentInChildren<ParticleSystemForceField>();
        }

        private void SetTarget (Transform newTarget)
        {
            _target = newTarget;
         
            if (_target)
            {
                _collectable = _target.GetComponent<Collectable>();
            }
        }


        public override void Tick(float deltaTime)
        {
            Transform target = _targetProvider.GetTarget();
            if (target != _target)
            {
                SetTarget(target);
            }
            TickCollection(deltaTime);
        }

        private void TickCollection(float deltaTime)
        {
            if (_target && _targeter.GetDistance() < AngleTreshold)
            {
                if (_collectable.TickCollection(deltaTime, _collectionRate.GetValue()))
                {
                    OnCollected.CallEvent(new Modification.Events.EventArgs(this, _collectable), this);
                    SetTarget(null);
                }
                UpdateBeam(true);
            }
            else
            {
                UpdateBeam(false);
            }
        }

        private void UpdateBeam(bool enable)
        {
            _beam.enabled = enable;
            _particleForceField.gameObject.SetActive(enable);
            if (_target && enable)
            {
                _beam.SetPosition(0, _beamStart.position);
                _beam.SetPosition(1, _target.position);

                float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg - 180f;
                _particleForceField.transform.position = _target.position;
                _particleForceField.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }
}
