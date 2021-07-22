using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Events.EventArgs;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using Lomztein.BFA2.Structures.Turrets.Targeters;
using Lomztein.BFA2.Structures.Turrets.TargetProviders;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Structures.Turrets.Weapons
{
    public class TurretWeaponBase : TurretComponent
    {
        public IWeapon Weapon;

        public ITargeter Targeter;
        public ITargetProvider Provider;
        public IRanger Ranger;

        [ModelAssetReference]
        public StatInfo DamageInfo;
        [ModelAssetReference]
        public StatInfo ProjectileAmountInfo;
        [ModelAssetReference]
        public StatInfo SpreadInfo;
        [ModelAssetReference]
        public StatInfo SpeedInfo;
        [ModelAssetReference]
        public StatInfo FirerateInfo;

        [ModelProperty]
        public float BaseDamage;
        [ModelProperty]
        public int BaseProjectileAmount;
        [ModelProperty]
        public float BaseSpread;
        [ModelProperty]
        public float BaseSpeed;
        [ModelProperty]
        public float BaseFirerate;

        public IStatReference Damage;
        public IStatReference ProjectileAmount;
        public IStatReference Spread;
        public IStatReference Speed;
        public IStatReference Firerate;

        public IEventCaller<EventArgs<IProjectile[]>> OnFire;
        public IEventCaller<EventArgs<IProjectile>> OnProjectile;
        public IEventCaller<EventArgs<HitInfo>> OnProjectileDepleted;
        public IEventCaller<EventArgs<HitInfo>> OnProjectileHit;
        public IEventCaller<EventArgs<HitInfo>> OnProjectileKill;

        public override StructureCategory Category => StructureCategories.Weapon;

        [ModelProperty]
        public Color Color;


        public override void End()
        {
        }

        public override void PreInit()
        {
            Weapon = GetComponent<IWeapon>();
            AddModdableAttribute(Modification.ModdableAttribute.Weapon);

            Damage = Stats.AddStat(DamageInfo, BaseDamage);
            ProjectileAmount = Stats.AddStat(ProjectileAmountInfo, BaseProjectileAmount);
            Spread = Stats.AddStat(SpreadInfo, BaseSpread);
            Speed = Stats.AddStat(SpeedInfo, BaseSpeed);
            Firerate = Stats.AddStat(FirerateInfo, BaseFirerate);

            OnFire = Events.AddEvent<EventArgs<IProjectile[]>>("OnFire", "On Depleted", "Executed when the projectile depletes its damage.");
            OnProjectile = Events.AddEvent<EventArgs<IProjectile>>("OnProjectile", "On Depleted", "Executed when the projectile depletes its damage.");
            OnProjectileDepleted = Events.AddEvent<EventArgs<HitInfo>>("OnProjectileDepleted", "On Depleted", "Executed when the projectile depletes its damage.");
            OnProjectileHit = Events.AddEvent<EventArgs<HitInfo>>("OnProjectileHit", "On Hit", "Executed when this weapon hits something.");
            OnProjectileKill = Events.AddEvent<EventArgs<HitInfo>>("OnProjectileKill", "On Kill", "Executed when this weapon kills something.");

            Weapon.OnFire += Weapon_OnFire;
            Weapon.OnProjectile += Weapon_OnProjectile;
            Weapon.OnProjectileDepleted += Weapon_OnProjectileDepleted;
            Weapon.OnProjectileHit += Weapon_OnProjectileHit;
            Weapon.OnProjectileKill += Weapon_OnProjectileKill;

            Damage.OnChanged += UpdateStats;
            ProjectileAmount.OnChanged += UpdateStats;
            Spread.OnChanged += UpdateStats;
            Speed.OnChanged += UpdateStats;
            Firerate.OnChanged += UpdateStats;
        }

        private void UpdateStats()
        {
            Weapon.Damage = Damage.GetValue();
            Weapon.ProjectileAmount = (int)ProjectileAmount.GetValue();
            Weapon.Spread = Spread.GetValue();
            Weapon.Speed = Speed.GetValue();
            Weapon.Firerate = Firerate.GetValue();
            Weapon.Range = GetRange();
        }

        public override void Init()
        {
            Targeter = GetComponentInParent<ITargeter>();
            Provider = GetComponentInParent<ITargetProvider>();
            Ranger = GetComponentInParent<IRanger>();
            UpdateStats();
        }

        private void Weapon_OnFire(IProjectile[] projs)
        {
            OnFire.CallEvent(new EventArgs<IProjectile[]>(projs));
        }

        private void Weapon_OnProjectile(IProjectile proj)
        {
            OnProjectile.CallEvent(new EventArgs<IProjectile>(proj));
        }

        private void Weapon_OnProjectileDepleted(HitInfo obj)
        {
            OnProjectileDepleted.CallEvent(new EventArgs<HitInfo>(obj));
        }

        private void Weapon_OnProjectileKill(HitInfo obj)
        {
            OnProjectileKill.CallEvent(new EventArgs<HitInfo>(obj));
        }

        private void Weapon_OnProjectileHit(HitInfo obj)
        {
            OnProjectileHit.CallEvent(new EventArgs<HitInfo>(obj));
        }

        public bool TryFire()
        {
            return Weapon.TryFire();
        }

        public Color GetColor()
        {
            return Color;
        }
        public float GetRange()
        {
            if (Ranger != null)
            {
                return Ranger.GetRange();
            }
            else
            {
                return 50;
            }
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}