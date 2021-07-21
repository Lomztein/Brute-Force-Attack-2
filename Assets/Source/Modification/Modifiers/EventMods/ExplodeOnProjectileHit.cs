using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.Misc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    public class ExplodeOnProjectileHit : ModBase
    {
        [ModelProperty]
        public float DamageMultBase;
        [ModelProperty]
        public float RangeBase;
        [ModelProperty]
        public float DamageMultStack;
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
            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute += Explode;

            _damageMult = stats.AddStat("ExplosionMultDamage", "Explosion Multiplier Damage", "The damage that the explosion does.", DamageMultBase);
            _range = stats.AddStat("ExplosionRangeBase", "Explosion Range", "The range of the explosion.", RangeBase);
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement("ExplosionMultDamage", new StatElement(this, DamageMultStack), Stat.Type.Multiplicative);
            stats.AddStatElement("ExplosionRangeBase", new StatElement(this, RangeStack), Stat.Type.Multiplicative);
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute -= Explode;
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement("ExplosionMultDamage", this, Stat.Type.Multiplicative);
            stats.RemoveStatElement("ExplosionRangeBase", this, Stat.Type.Multiplicative);
        }

        private void Explode(HitEventArgs args)
        {
            float damage = args.Info.DamageInfo.Damage * _damageMult.GetValue();
            float range = ComputeDiameter (_range.GetValue(), 1f);

            Explosion explosion = ExplosionPrefab.Instantiate().GetComponent<Explosion>();
            explosion.transform.position = args.Info.Point;
            explosion.Explode(damage, range);

            if (DepleteProjectile)
            {
                args.Info.Projectile.Deplete();
            }
        }

        private float ComputeDiameter(float area, float scalar)
            => Mathf.Max(Mathf.Log(Mathf.Sqrt(area * scalar) / Mathf.PI, 10f), 0.75f);
    }
}