using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation.FireAnimations
{
    public class SimpleFireAnimation : FireAnimation
    {
        [ModelProperty]
        public ContentSpriteReference[] AnimationSprites;

        public override void Play (float animSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(StartAnimation(GetAnimationDelay(AnimationSprites, animSpeed)));
        }

        private IEnumerator StartAnimation (float delay)
        {
            yield return Animate(AnimationSprites, delay);
            ResetSprite();
        }
    }
}
