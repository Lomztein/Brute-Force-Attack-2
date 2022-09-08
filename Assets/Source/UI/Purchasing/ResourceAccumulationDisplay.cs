using Lomztein.BFA2.Purchasing.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Purchasing.UI
{
    public class ResourceAccumulationDisplay : MonoBehaviour
    {
        public GameObject ResourceContainer;
        private IResourceContainer _container;
        public Resource Resource;
        public Text Text;

        private int _accumulation;
        private float _lastChangeTime;
        public float FadeTime;

        private void Awake()
        {
            _container = ResourceContainer.GetComponent<IResourceContainer>();
            _container.OnResourceChanged += OnResourceChanged;
        }

        private void OnResourceChanged(Resource resource, int prev, int cur)
        {
            if (resource.Equals(Resource))
            {
                Accumulate(cur - prev);
            }
        }

        private void Update()
        {
            var color = Text.color;
            float timeSince = Time.time - _lastChangeTime;
            float a = 1f - Mathf.Clamp01 (timeSince / FadeTime);
            if (a < float.Epsilon)
            {
                _accumulation = 0;
            }
            Text.color = new Color(color.r, color.g, color.b, a);
        }

        private void Accumulate(int change)
        {
            _accumulation += change;
            _lastChangeTime = Time.time;
            string prefix = _accumulation > 0 ? "+" : "";
            Text.text = $"{prefix}{_accumulation}";
        }
    }
}
