using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
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
        [ModelAssetReference]
        public EventInfo EventInfo;
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
            events.GetEvent(EventInfo.Identifier).Event.AddListener(Explode, this);

            _damageMult = stats.AddStat(ExplosionDamageFactorInfo, DamageFactorBase, this);
            _range = stats.AddStat(ExplosionRangeInfo, RangeBase * Coeffecient, this);
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement(ExplosionDamageFactorInfo.Identifier, new StatElement(this, DamageFactorStack), this);
            stats.AddStatElement(ExplosionRangeInfo.Identifier, new StatElement(this, RangeStack * Coeffecient), this);
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(EventInfo.Identifier).Event.RemoveListener(Explode, this);
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement(ExplosionDamageFactorInfo.Identifier, this, this);
            stats.RemoveStatElement(ExplosionRangeInfo.Identifier, this, this);
        }

        private void Explode(Events.EventArgs args)
        {
            HitInfo hitInfo = args.GetArgs<HitInfo>();

            float damage = hitInfo.DamageInfo.Damage * _damageMult.GetValue();
            float range = _range.GetValue();

            Explosion explosion = ExplosionPrefab.Instantiate().GetComponent<Explosion>();
            explosion.transform.position = hitInfo.Point;

            if (DepleteProjectile)
            {
                hitInfo.Projectile.Deplete();
            }
        }
    }
}