using Lomztein.BFA2.Animation.FireAnimations;
using Lomztein.BFA2.Colorization;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Turrets.Rangers;
using Lomztein.BFA2.Turrets.Targeters;
using Lomztein.BFA2.Turrets.TargetProviders;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.Projectiles;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Color = Lomztein.BFA2.Colorization.Color;

namespace Lomztein.BFA2.Turrets.Weapons
{
    public class TurretWeapon : TurretComponent, IColorProvider
    {
        [TurretComponent]
        public ITargeter Targeter;
        [TurretComponent]
        public ITargetProvider Provider;
        [TurretComponent]
        public IRanger Ranger;

        [ModelProperty]
        public float FireTreshold;
        public LayerMask HitLayer;

        public IStatReference Damage;
        public IStatReference ProjectileAmount;
        public IStatReference Spread;
        public IStatReference Speed;
        public IStatReference Firerate;
        public float Cooldown => 1f / Firerate.GetValue();

        private IWeaponFire _weaponFire;
        private IFireAnimation _fireAnimation;
        public Color Color;
        private bool _chambered;

        public override void End()
        {
        }

        public override void Init()
        {
            Rechamber();
            _weaponFire = GetComponent<WeaponFire>();
            _fireAnimation = GetComponent<IFireAnimation>() ?? new NoFireAnimation();

            Damage = Stats.AddStat("Damage", "Damage", "The damage each projectile does.");
            ProjectileAmount = Stats.AddStat("ProjectileAmount", "Projectile Amount", "How many projectiles are fired at once.");
            Spread = Stats.AddStat("Spread", "Spread", "How much the projectiles spread.");
            Speed = Stats.AddStat("Speed", "Speed", "How fast the projectiles fly.");
            Firerate = Stats.AddStat("Firerate", "Firerate", "How quickly the weapon fires.");
        }

        private void Rechamber ()
        {
            _chambered = true;
        }

        public override void Tick(float deltaTime)
        {
            if (Targeter != null && Targeter.GetDistance () < FireTreshold)
            {
                if (_chambered) {
                    Fire();
                    _chambered = false;
                    Invoke("Rechamber", Cooldown);
                }
            }
        }

        private void Fire ()
        {
            IProjectileInfo info = new ProjectileInfo();
            info.Color = Color;
            info.Damage = Damage.GetValue ();
            info.Layer = HitLayer;
            info.Target = Provider?.GetTarget();
            info.Range = Ranger?.GetRange() ?? 50f;

            _fireAnimation.Play(Cooldown);
            _weaponFire.Fire(info, Speed.GetValue (), Spread.GetValue(), (int)ProjectileAmount.GetValue());
        }

        public Color GetColor()
        {
            return Color;
        }
    }
}