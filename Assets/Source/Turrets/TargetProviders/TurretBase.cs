using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Targeting;
using Lomztein.BFA2.Turrets.Rangers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.TargetProviders
{
    public class TurretBase : TurretComponent, ITargetProvider, IRanger
    {
        [ModelProperty]
        public int SimultaniousTargets;
        [ModelProperty]
        public float Range;
        public LayerMask TargetLayer;

        private Transform _target;

        private ITargetFinder _targetFinder;

        public IEventCaller<TargetEventArgs> OnTargetAcquired { get; private set; }
        private IEventReference<TargetEventArgs> _targetAcquiredTest;

        public override void End()
        {
        }

        public float GetRange()
        {
            return Range;
        }

        public Transform GetTarget()
        {
            return _target;
        }

        public override void Init()
        {
            _targetFinder = GetComponent<ITargetFinder>();
            OnTargetAcquired = Events.AddEvent<TargetEventArgs>("OnTargetAcquired", "On Target Acquired", "Executed whenever this base acquires a target.");
            _targetAcquiredTest = Events.GetEvent<TargetEventArgs>("OnTargetAcquired");
            _targetAcquiredTest.Event.OnExecute += OnTargetAcquiredTest;
        }

        private void OnTargetAcquiredTest(TargetEventArgs obj)
        {
            Debug.Log("Found target: " + obj.Target);
        }

        public override void Tick(float deltaTime)
        {
            if (_target == null)
            {
                _target = _targetFinder.FindTarget (Physics2D.OverlapCircleAll(transform.position, Range, TargetLayer));
                if (_target != null)
                {
                    OnTargetAcquired.CallEvent(new TargetEventArgs() { Target = _target });
                }
            }
            else
            {
                if ((_target.position - transform.position).sqrMagnitude > Range * Range)
                {
                    _target = null;
                }
            }
        }
    }
}