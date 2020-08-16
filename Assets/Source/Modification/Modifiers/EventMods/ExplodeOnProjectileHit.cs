using Lomztein.BFA2.Content.References;
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
    public class ExplodeOnProjectileHit : BaseModComponent
    {
        [ModelProperty]
        public float DamageBase;
        [ModelProperty]
        public float RangeBase;
        [ModelProperty]
        public float DamageStack;
        [ModelProperty]
        public float RangeStack;
        [ModelProperty]
        public ContentPrefabReference ExplosionPrefab;
        [ModelProperty]
        public bool DestroyProjectile;

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            stats.AddStat("ExplodeDamageFactor", "Explosion Damage Factor", "How much damage the explosion does.", DamageBase);
            stats.AddStat("ExplodeRangeFactor", "Explosion Range Factor", "How far the explosion reaches.", RangeBase);

            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute += Explode;
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement("ExplodeDamageFactor", new StatElement(this, DamageStack), Stat.Type.Multiplicative);
            stats.AddStatElement("ExplodeRangeFactor", new StatElement(this, RangeStack), Stat.Type.Multiplicative);
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute -= Explode;
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement("ExplodeDamageFactor", this, Stat.Type.Multiplicative);
            stats.RemoveStatElement("ExplodeRangeFactor", this, Stat.Type.Multiplicative);
        }

        private void Explode(HitEventArgs args)
        {
            float damage = args.Info.DamageInfo.Damage;
            float range = ComputeDiameter(args.Info.DamageInfo.Damage, 1f) / 2f;

            Explosion explosion = ExplosionPrefab.Instantiate().GetComponent<Explosion>();
            explosion.transform.position = args.Info.Point;
            explosion.Explode(damage, range);

            if (DestroyProjectile)
            {
                args.Info.Projectile.End();
            }
        }

        private float ComputeDiameter(float area, float scalar)
            => Mathf.Max(Mathf.Log(Mathf.Sqrt(area * scalar) / Mathf.PI, 2f), 1f);
    }
}