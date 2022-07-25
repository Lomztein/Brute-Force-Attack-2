using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Visuals.Effects
{
    public class TrailEffect : Effect
    {
        private const float MIN_PLAY_TIME = 0.1f;

        public TrailRenderer Trail;

        private void Awake()
        {
            Trail = GetComponent<TrailRenderer>();
        }

        public override bool IsPlaying => Time.time < _stopTime + Mathf.Max(Trail.time, MIN_PLAY_TIME);
        private float _stopTime;

        public override void Play()
        {
            base.Play();
            Trail.emitting = true;
        }

        public override void Stop()
        {
            Trail.emitting = false;
            _stopTime = Time.time;
        }
    }
}