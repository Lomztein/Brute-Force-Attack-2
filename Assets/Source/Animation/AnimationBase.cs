using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Animation
{
    public abstract class AnimationBase : MonoBehaviour, IAnimation
    {
        private SpriteRenderer _spriteRenderer;
        [ModelProperty]
        public SpriteSheet SpriteSheet;
        [ModelProperty]
        public Vector2Int DefaultSpriteIndex;
        [ModelProperty]
        public float PlaySpeedMultiplier = 1f;

        public bool IsPlaying { get; protected set; }

        public abstract void Play(float animSpeed);

        protected float GetAnimationDelay(int spriteCount, float animationTime)
        {
            return animationTime / spriteCount;
        }

        protected IEnumerator Animate(IEnumerable<Sprite> sprites, float delay)
        {
            foreach (Sprite sprite in sprites)
            {
                GetSpriteRenderer().sprite = sprite;
                yield return new WaitForSeconds(delay / PlaySpeedMultiplier);
            }
        }

        private SpriteRenderer GetSpriteRenderer ()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        }

        protected void ResetSprite()
        {
            GetSpriteRenderer().sprite = SpriteSheet.GetSprite(DefaultSpriteIndex.x, DefaultSpriteIndex.y);
        }
    }
}
