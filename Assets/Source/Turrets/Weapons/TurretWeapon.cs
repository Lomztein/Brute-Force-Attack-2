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

namespace Lomztein.BFA2.Turrets.Weapons
{
    public class TurretWeapon : TurretComponent
    {
        [TurretComponent]
        public ITargeter Targeter;
        [TurretComponent]
        public ITargetProvider Provider;
        [TurretComponent]
        public IRanger Ranger;

        [ModelProperty]
        public float FireTreshold;
        [ModelProperty]
        public float Damage;
        [ModelProperty]
        public int ProjectileAmount;
        [ModelProperty]
        public int HitLayer;
        [ModelProperty]
        public float Deviation;
        [ModelProperty]
        public float Speed;
        [ModelProperty]
        public float Firerate;
        public float Cooldown => 1f / Firerate;

        private IWeaponFire _weaponFire;
        public ColorCache Color;
        private bool _chambered;

        public override void End()
        {
        }

        public override void Init()
        {
            Rechamber();
            _weaponFire = GetComponent<WeaponFire>();
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
            info.Color = Color.Get();
            info.Damage = Damage;
            info.Layer = HitLayer;
            info.Target = Provider?.GetTargets()?.FirstOrDefault();
            info.Range = 50f;

            _weaponFire.Fire(info, Speed, Deviation, ProjectileAmount);
        }
    }
}