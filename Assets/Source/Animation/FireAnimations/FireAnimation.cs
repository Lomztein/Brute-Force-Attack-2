using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation.FireAnimations
{
    public abstract class FireAnimation : MonoBehaviour, IFireAnimation
    {
        public SpriteRenderer AnimatedSprite;
        public Sprite DefaultSprite;
        public float PlaySpeedMultiplier = 1f;

        public bool IsPlaying { get; protected set; }

        public abstract void Play(float animSpeed);

        protected float GetAnimationDelay(Sprite[] sprites, float animationLength)
        {
            return animationLength / sprites.Length;
        }

        protected IEnumerator Animate(Sprite[] sprites, float delay)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                AnimatedSprite.sprite = sprites[i];
                yield return new WaitForSeconds(delay / PlaySpeedMultiplier);
            }
        }

        protected void ResetSprite()
        {
            AnimatedSprite.sprite = DefaultSprite;
        }
    }
}
