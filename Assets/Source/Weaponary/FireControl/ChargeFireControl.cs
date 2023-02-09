using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.FireControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Weaponary.FireControl
{
    public class ChargeFireControl : MonoBehaviour, IFireControl
    {
        private IWeapon _weapon;
        private float _baseFirerate;
        private bool _charging;
        private float _currentCharge;

        [ModelProperty]
        public float ChargeRate;
        [ModelProperty]
        public float MaxCharge;
        [ModelProperty]
        public bool ResetChargeOnFire;

        private void Awake()
        {
            _weapon = GetComponent<IWeapon>();
            _baseFirerate = _weapon.Firerate;
            _weapon.OnFire += Weapon_OnFire;
        }

        private void Weapon_OnFire(Projectiles.IProjectile[] obj, object source)
        {
            if (ResetChargeOnFire)
            {
                _currentCharge = 0;
            }
        }

        private float GetChargeRate()
            => ChargeRate * _weapon.Firerate / _baseFirerate;

        public float GetCharge()
            => Mathf.Clamp(_currentCharge, 0f, MaxCharge);

        public float GetCharge01()
            => GetCharge() / MaxCharge;

        public bool TryFire()
        {
            _charging = true;
            return _currentCharge >= MaxCharge;
        }

        private void FixedUpdate()
        {
            if (_charging)
            {
                _currentCharge += GetChargeRate() * Time.fixedDeltaTime;
                _charging = false;
            }
            else
            {
                _currentCharge -= GetChargeRate() * Time.fixedDeltaTime;
                if (_currentCharge <= 0f) _currentCharge = 0f;
            }
        }
    }
}
