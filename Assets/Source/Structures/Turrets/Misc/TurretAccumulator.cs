using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Structures.Turrets.Weapons;
using Lomztein.BFA2.Weaponary;
using Lomztein.BFA2.Weaponary.FireSynchronization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Turrets.Misc
{
    public class TurretAccumulator : TurretComponent, IFireControl
    {
        [ModelAssetReference]
        public StatInfo ChargeTime;
        [ModelProperty]
        public float ChargeTimeBase;
        private IStatReference _chargeTime;
        [ModelAssetReference]
        public StatInfo FireTime;
        [ModelProperty]
        public float FireTimeBase;
        private IStatReference _fireTime;

        private float _chargeCooldown;
        private float _fireCooldown;

        private bool Charged => _chargeCooldown <= 0f;
        private bool CanFire => _fireCooldown > 0f;
        public override StructureCategory Category => StructureCategories.Utility;

        public override void End()
        {
        }

        public override void PreInit()
        {
            base.PreInit();
            _chargeTime = Stats.AddStat(ChargeTime, ChargeTimeBase, this);
            _fireTime = Stats.AddStat(FireTime, FireTimeBase, this);
        }

        public override void PostInit()
        {
            AttachChildren();
        }

        public override void Init()
        {
        }

        public override void Tick(float deltaTime)
        {
            _chargeCooldown -= deltaTime;
            _fireCooldown -= deltaTime;
        }

        public void OnHierarchyChanged ()
        {
            AttachChildren();
        }

        private void AttachChildren()
        {
            foreach (Transform child in transform)
            {
                Weapon weapon = child.GetComponent<Weapon>();
                if (weapon)
                {
                    weapon.AddFireControl(this);
                }
            }
        }

        public bool TryFire()
        {
            if (Charged)
            {
                _fireCooldown = _fireTime.GetValue();
            }
            if (CanFire)
            {
                _chargeCooldown = _chargeTime.GetValue();
            }

            return CanFire;
        }
    }
}
