using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Modification.Stats;
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
        public IStatReference Range;
        [ModelProperty]
        public LayerMask TargetLayer;

        private Transform _target;

        private ITargetFinder _targetFinder;

        private IEventCaller<TargetEventArgs> _onTargetAcquired;

        public override void End()
        {
        }

        public float GetRange()
        {
            return Range.GetValue();
        }

        public Transform GetTarget()
        {
            return _target;
        }

        public override void Init()
        {
            _targetFinder = GetComponent<ITargetFinder>();
            _onTargetAcquired = Events.AddEvent<TargetEventArgs>("OnTargetAcquired", "On Target Acquired", "Executed whenever this base acquires a target.");
            Range = Stats.AddStat("Range", "Range", "The range that this base can acquire targets at.");

            AddAttribute(Modification.ModdableAttribute.Ranged);
        }

        public override void Tick(float deltaTime)
        {
            float range = Range.GetValue();
            if (_target == null)
            {
                _target = _targetFinder.FindTarget (Physics2D.OverlapCircleAll(transform.position, range, TargetLayer));
                if (_target != null)
                {
                    _onTargetAcquired.CallEvent(new TargetEventArgs() { Target = _target });
                }
            }
            else
            {
                if ((_target.position - transform.position).sqrMagnitude > range * range)
                {
                    _target = null;
                }
            }
        }
    }
}