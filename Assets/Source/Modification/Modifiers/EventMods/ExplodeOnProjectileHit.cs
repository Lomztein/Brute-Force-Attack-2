using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Misc;
using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    public class ExplodeOnProjectileHit : ModBase
    {
        [ModelProperty]
        public StatInfo ExplosionDamageFactorInfo;
        [ModelProperty]
        public StatInfo ExplosionRangeInfo;
        [ModelProperty]
        public float DamageFactorBase;
        [ModelProperty]
        public float RangeBase;
        [ModelProperty]
        public float DamageFactorStack;
        [ModelProperty]
        public float RangeStack;
        [ModelProperty]
        public ContentPrefabReference ExplosionPrefab = new ContentPrefabReference();
        [ModelProperty]
        public bool DepleteProjectile;

        private IStatReference _damageMult;
        private IStatReference _range;

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<EventArgs<HitInfo>>("OnProjectileHit").Event.OnExecute += Explode;

            _damageMult = stats.AddStat(ExplosionDamageFactorInfo, DamageFactorBase * Coeffecient);
            _range = stats.AddStat(ExplosionRangeInfo, RangeBase);
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement(ExplosionDamageFactorInfo.Identifier, new StatElement(this, DamageFactorStack * Coeffecient));
            stats.AddStatElement(ExplosionRangeInfo.Identifier, new StatElement(this, RangeStack));
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<EventArgs<HitInfo>>("OnProjectileHit").Event.OnExecute -= Explode;
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement(ExplosionDamageFactorInfo.Identifier, this);
            stats.RemoveStatElement(ExplosionRangeInfo.Identifier, this);
        }

        private void Explode(EventArgs<HitInfo> args)
        {
            float damage = args.Object.DamageInfo.Damage * _damageMult.GetValue();
            float range = ComputeDiameter (_range.GetValue(), 1f);

            Explosion explosion = ExplosionPrefab.Instantiate().GetComponent<Explosion>();
            explosion.transform.position = args.Object.Point;
            explosion.Explode(damage, range);

            if (DepleteProjectile)
            {
                args.Object.Projectile.Deplete();
            }
        }

        private float ComputeDiameter(float area, float scalar)
            => Mathf.Max(Mathf.Log(Mathf.Sqrt(area * scalar) / Mathf.PI, 10f), 0.75f);
    }
}