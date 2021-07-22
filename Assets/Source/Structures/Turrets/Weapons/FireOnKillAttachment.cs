using Lomztein.BFA2.Modification.Events.EventArgs;
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
    public class FireOnKillAttachment : TurretWeaponBase
    {
        public TurretWeaponBase Parent;
        public int ToFire;

        [ModelAssetReference]
        public StatInfo RangeInfo;
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
                Parent.Events.GetEvent<EventArgs<HitInfo>>("OnProjectileKill").Event.OnExecute -= OnParentKill;
            }
        }

        public override void Init()
        {
            base.Init();
            Range = Stats.AddStat(RangeInfo, BaseRange);
            Parent = transform.parent.GetComponent<TurretWeaponBase>();
            if (Parent)
            {
                Parent.Events.GetEvent<EventArgs<HitInfo>>("OnProjectileKill").Event.OnExecute += OnParentKill;
            }
        }

        private void OnParentKill(EventArgs<HitInfo> obj)
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
