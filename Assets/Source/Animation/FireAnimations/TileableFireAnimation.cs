using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation.FireAnimations
{
    public class TileableFireAnimation : FireAnimation
    {
        [ModelProperty]
        public ContentSpriteReference[] StartAnimationSprites;
        [ModelProperty]
        public ContentSpriteReference[] MidAnimationSprites;
        [ModelProperty]
        public ContentSpriteReference[] EndAnimationSprites;

        private Coroutine _coroutine;
        private bool _continue;

        public override void Play(float animSpeed)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            float delay = GetAnimationDelay(MidAnimationSprites, animSpeed);
            _coroutine = StartCoroutine(PlayInternal(delay));
        }

        private IEnumerator PlayInternal (float delay)
        {
            if (_continue == false)
            {
                yield return StartAnimation(delay);
                yield return EndAnimation(delay);
                _continue = true;
            }
            else
            {
                yield return ContinueAnimation(delay);
                yield return EndAnimation(delay);
                _continue = false;
            }
        }

        private IEnumerator StartAnimation(float delay)
        {
            yield return Animate(StartAnimationSprites, delay);
        }

        private IEnumerator ContinueAnimation(float delay)
        {
            yield return Animate(MidAnimationSprites, delay);
        }

        private IEnumerator EndAnimation (float delay)
        {
            yield return Animate(EndAnimationSprites, delay);
            ResetSprite();
        }
    }
}
