using Lomztein.BFA2.Player.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class HealthSlider : MonoBehaviour
    {
        protected IHealthContainer _healthContainer;
        public Slider Slider;

        private void Start()
        {
            _healthContainer = GetComponent<IHealthContainer>();
            _healthContainer.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(0f, _healthContainer.GetCurrentHealth(), _healthContainer.GetCurrentHealth());
        }

        protected virtual void OnHealthChanged(float arg1, float arg2, float arg3)
        {
            Slider.value = _healthContainer.GetCurrentHealth() / _healthContainer.GetMaxHealth();
        }
    }
}
