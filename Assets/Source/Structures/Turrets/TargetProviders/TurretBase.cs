using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Placement;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using Lomztein.BFA2.Targeting;
using Lomztein.BFA2.UI.ContextMenu;
using Lomztein.BFA2.UI.ContextMenu.Providers;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.TargetProviders
{
    public class TurretBase : TurretComponent, ITargetProvider, IRanger
    {
        [ModelAssetReference]
        public StatInfo RangeInfo;
        [ModelAssetReference]
        public EventInfo OnTargetAcquiredInfo;
        [ModelProperty]
        public float BaseRange;
        public IStatReference Range;

        [ModelProperty]
        public LayerMask TargetLayer;

        private Transform _target;
        private TargetFinder _targetFinder;

        private IEventCaller _onTargetAcquired;

        public override StructureCategory Category => StructureCategories.TargetFinder;

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

        public override void PreInit()
        {
            AddTag("Base");
            _onTargetAcquired = Events.AddEvent(OnTargetAcquiredInfo);
            Range = Stats.AddStat(RangeInfo, BaseRange);
        }

        public override void Init()
        {
            _targetFinder = GetComponent<TargetFinder>();
        }

        public void SetEvaluator(TargetEvaluator evaluator)
            => _targetFinder.SetEvaluator(evaluator);
        private bool IsWithinRange(Transform trans, float range)
            => (trans.position - transform.position).sqrMagnitude < range * range;

        public override void Tick(float deltaTime)
        {
            float range = GetRange();
            
            if (_target == null)
            {
                _target = _targetFinder.FindTarget(gameObject, Physics2D.OverlapCircleAll(transform.position, range, TargetLayer).Where(x => IsWithinRange(x.transform, range)));
                if (_target != null)
                {
                    _onTargetAcquired.CallEvent(new Modification.Events.EventArgs(this, _target));
                }
            }
            if (_target && !IsWithinRange(_target, range))
            {
                _target = null;
            }
        }


    }
}