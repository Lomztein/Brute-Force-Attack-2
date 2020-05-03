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
        }

        public override void Tick(float deltaTime)
        {
            if (_target == null)
            {
                _target = _targetFinder.FindTarget (Physics2D.OverlapCircleAll(transform.position, Range, TargetLayer));
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