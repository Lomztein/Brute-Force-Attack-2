using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    public class CameraBackgroundColorizer : MonoBehaviour
    {
        private IHealthContainer _healthContainer;
        private Camera _camera;

        public Gradient Colors;

        private void Awake ()
        {
            _healthContainer = GetComponent<IHealthContainer>();
            _camera = GetComponent<Camera>();

            _healthContainer.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float before, float after, float max, object source)
        {
            _camera.backgroundColor = Colors.Evaluate(Mathf.Clamp01(after / max));
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                GetComponent<Camera>().backgroundColor = Colors.Evaluate(1);
            }
        }

        public void UpdateColors ()
        {
            OnHealthChanged(_healthContainer.GetCurrentHealth(), _healthContainer.GetCurrentHealth(), _healthContainer.GetMaxHealth(), this);
        }
    }
}
