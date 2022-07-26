using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Visuals.Effects
{
    public class CompositeEffect : Effect
    {
        public Effect[] Effects;

        public override bool IsPlaying => Effects.Any(x => x.IsPlaying);

        private void Awake()
        {
            var list = new List<Effect>();
            foreach (Transform child in transform)
            {
                list.Add(child.GetComponent<Effect>());
            }
            Effects = list.ToArray();
        }

        public override void Play()
        {
            base.Play();
            foreach (Effect effect in Effects)
            {
                effect.Play();
            }
        }

        public override void Stop()
        {
            foreach (Effect effect in Effects)
            {
                effect.Stop();
            }
        }
    }
}