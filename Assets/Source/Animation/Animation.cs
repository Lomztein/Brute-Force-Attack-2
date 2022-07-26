using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Animation
{
    public class Animation : AnimationBase
    {
        [ModelProperty]
        public Vector2Int[] FrameIndecies;

        public override void Play(float animSpeed)
        {
            StopAllCoroutines();
            var sprites = SpriteSheet.GetSprites(FrameIndecies);
            float delay = GetAnimationDelay(FrameIndecies.Length, animSpeed);
            StartCoroutine(StartAnimation(sprites, delay));
        }

        private IEnumerator StartAnimation(IEnumerable<Sprite> sprites, float delay)
        {
            yield return Animate(sprites, delay);
            ResetSprite();
        }
    }
}
