using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Health
{
    [ExecuteAlways]
    public class CameraBackgroundColorizer : MonoBehaviour
    {
        private IHealthContainer _healtContainer;
        private Camera _camera;

        public Gradient Colors;

        private void Awake ()
        {
            _healtContainer = GetComponent<IHealthContainer>();
            _camera = GetComponent<Camera>();

            _healtContainer.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float before, float after, float max)
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
    }
}
