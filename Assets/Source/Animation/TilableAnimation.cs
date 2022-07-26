using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation
{
    public class TilableAnimation : AnimationBase
    {
        [ModelProperty]
        public Vector2Int[] StartAnimationIndecies;
        [ModelProperty]
        public Vector2Int[] MidAnimationIndecies;
        [ModelProperty]
        public Vector2Int[] EndAnimationIndecies;

        private Coroutine _coroutine;
        private bool _continue;

        public override void Play(float animSpeed)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            float delay = GetAnimationDelay(StartAnimationIndecies.Length, animSpeed);
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
            yield return Animate(SpriteSheet.GetSprites(StartAnimationIndecies), delay);
        }

        private IEnumerator ContinueAnimation(float delay)
        {
            yield return Animate(SpriteSheet.GetSprites(MidAnimationIndecies), delay);
        }

        private IEnumerator EndAnimation (float delay)
        {
            yield return Animate(SpriteSheet.GetSprites(EndAnimationIndecies), delay);
            ResetSprite();
        }
    }
}
