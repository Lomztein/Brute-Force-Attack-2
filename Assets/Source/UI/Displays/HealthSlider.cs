using Lomztein.BFA2.Player.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class HealthSlider : MonoBehaviour
    {
        private IHealthContainer _healthContainer;
        public Slider Slider;

        private void Start()
        {
            _healthContainer = GetComponent<IHealthContainer>();
        }

        void Update()
        {
            Slider.value = _healthContainer.GetCurrentHealth() / _healthContainer.GetMaxHealth(); 
        }
    }
}
