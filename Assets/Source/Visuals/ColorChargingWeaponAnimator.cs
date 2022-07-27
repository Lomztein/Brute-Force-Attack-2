using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Weaponary.FireControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Visuals
{
    public class ColorChargingWeaponAnimator : MonoBehaviour
    {
        [ModelReference]
        public ChargeFireControl Weapon;
        [ModelProperty]
        public string ColorProperty;
        [ModelProperty]
        public Color EmissionColor;
        [ModelProperty]
        public float MaxIntensity;
        [ModelProperty]
        public bool HideOnLowCharge;

        private Renderer _renderer;
        private Material _material;
        private Vector4 _color;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = Instantiate(_renderer.material);
            _renderer.material = _material;
            _color = new Vector4(EmissionColor.r, EmissionColor.g, EmissionColor.b, EmissionColor.a);
        }

        private void FixedUpdate()
        {
            float factor = Weapon.GetCharge01();
            _material.SetColor(ColorProperty, factor * MaxIntensity * _color);
            if (HideOnLowCharge)
            {
                _renderer.enabled = factor > float.Epsilon;
            }
        }
    }
}
