using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Visuals.Effects
{
    public class ParticleEffect : Effect
    {
        public ParticleSystem System;

        private void Awake()
        {
            System = GetComponent<ParticleSystem>();
        }

        public override bool IsPlaying => System.IsAlive(true);

        public override void Play()
        {
            base.Play();
            System.Play();
        }

        public override void Stop()
        {
            System.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}