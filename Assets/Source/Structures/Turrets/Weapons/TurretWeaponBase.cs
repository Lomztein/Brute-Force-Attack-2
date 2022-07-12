using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Modification.Events;
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

        [ModelAssetReference]
        public EventInfo OnFireInfo;
        [ModelAssetReference]
        public EventInfo OnProjectileInfo;
        [ModelAssetReference]
        public EventInfo OnProjectileDepletedInfo;
        [ModelAssetReference]
        public EventInfo OnProjectileHitInfo;
        [ModelAssetReference]
        public EventInfo OnProjectileKillInfo;

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

        public IEventCaller OnFire;
        public IEventCaller OnProjectile;
        public IEventCaller OnProjectileDepleted;
        public IEventCaller OnProjectileHit;
        public IEventCaller OnProjectileKill;

        private bool _statsInitialized;

        public override StructureCategory Category => StructureCategories.Weapon;

        [ModelProperty]
        public Color Color;


        public override void End()
        {
        }

        public override void PreInit()
        {
            Weapon = GetComponent<IWeapon>();
            AddTag("Weapon");

            Damage = Stats.AddStat(DamageInfo, BaseDamage, this);
            ProjectileAmount = Stats.AddStat(ProjectileAmountInfo, BaseProjectileAmount, this);
            Spread = Stats.AddStat(SpreadInfo, BaseSpread, this);
            Speed = Stats.AddStat(SpeedInfo, BaseSpeed, this);
            Firerate = Stats.AddStat(FirerateInfo, BaseFirerate, this);
            _statsInitialized = true;

            OnFire = Events.AddEvent(OnFireInfo, this);
            OnProjectile = Events.AddEvent(OnProjectileInfo, this);
            OnProjectileDepleted = Events.AddEvent(OnProjectileDepletedInfo, this);
            OnProjectileHit = Events.AddEvent(OnProjectileHitInfo, this);
            OnProjectileKill = Events.AddEvent(OnProjectileKillInfo, this);

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

            UpdateStats();

            Weapon.Init();
        }

        private void UpdateStats()
        {
            if (_statsInitialized)
            {
                Weapon.Damage = Damage.GetValue();
                Weapon.ProjectileAmount = (int)ProjectileAmount.GetValue();
                Weapon.Spread = Spread.GetValue();
                Weapon.Speed = Speed.GetValue();
                Weapon.Firerate = Firerate.GetValue();
                Weapon.Range = GetRange();
            }
        }

        public override void Init()
        {
            Targeter = GetComponentInParent<ITargeter>();
            Provider = GetComponentInParent<ITargetProvider>();
            Ranger = GetComponentInParent<IRanger>();
            UpdateStats();
        }

        public virtual void OnHierarchyChanged ()
        {
            UpdateStats();
        }

        public virtual void OnStatChanged()
        {
            UpdateStats();
        }

        private void Weapon_OnFire(IProjectile[] projs)
        {
            OnFire.CallEvent(new Modification.Events.EventArgs(this, projs));
        }

        private void Weapon_OnProjectile(IProjectile proj)
        {
            OnProjectile.CallEvent(new Modification.Events.EventArgs(this, proj));
        }

        private void Weapon_OnProjectileDepleted(HitInfo obj)
        {
            OnProjectileDepleted.CallEvent(new Modification.Events.EventArgs(this, obj));
        }

        private void Weapon_OnProjectileKill(HitInfo obj)
        {
            OnProjectileKill.CallEvent(new Modification.Events.EventArgs(this, obj));
        }

        private void Weapon_OnProjectileHit(HitInfo obj)
        {
            OnProjectileHit.CallEvent(new Modification.Events.EventArgs(this, obj));
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