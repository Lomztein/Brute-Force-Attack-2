using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Weapons
{
    public class FireOnEventTurretWeapon : TurretWeaponBase
    {
        public TurretWeaponBase Parent;
        public int ToFire;

        [ModelAssetReference]
        public StatInfo RangeInfo;
        [ModelAssetReference]
        public EventInfo EventInfo;
        [ModelProperty]
        public float BaseRange;
        public IStatReference Range;
        [ModelProperty]
        public LayerMask TargetLayerMask;

        public override void End()
        {
            base.End();
            if (Parent)
            {
                Parent.Events.GetEvent(EventInfo.Identifier).Event.RemoveListener(OnParentKill, this);
            }
        }

        public override void Init()
        {
            base.Init();
            Range = Stats.AddStat(RangeInfo, BaseRange, this);
            Parent = transform.parent.GetComponent<TurretWeaponBase>();
            if (Parent)
            {
                Parent.Events.GetEvent(EventInfo.Identifier).Event.AddListener(OnParentKill, this);
            }
        }

        private void OnParentKill(Modification.Events.EventArgs obj)
        {
            ToFire++;
        }

        public override void Tick(float deltaTime)
        {
            if (ToFire > 0 && Weapon.CanFire())
            {
                var targets = Physics2D.OverlapCircleAll(transform.position, Range.GetValue(), TargetLayerMask);
                if (targets.Length > 0)
                {
                    Weapon.Target = targets[UnityEngine.Random.Range(0, targets.Length)].transform;
                    Weapon.Range = Range.GetValue();
                    if (TryFire())
                    {
                        ToFire--;
                    }
                }
            }
        }
    }
}
