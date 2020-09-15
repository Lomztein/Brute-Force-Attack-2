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
    public class ExplodeOnProjectileHit : BaseModComponent
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
        public ContentPrefabReference ExplosionPrefab;
        [ModelProperty]
        public bool DestroyProjectile;

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute += Explode;
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            events.GetEvent<HitEventArgs>("OnHit").Event.OnExecute -= Explode;
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
        }

        private void Explode(HitEventArgs args)
        {
            float damage = args.Info.DamageInfo.Damage * DamageMultBase;
            float range = RangeBase / 2f;

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