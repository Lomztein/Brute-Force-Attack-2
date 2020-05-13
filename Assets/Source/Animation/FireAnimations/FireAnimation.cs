using Lomztein.BFA2.Content.References;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation.FireAnimations
{
    public abstract class FireAnimation : MonoBehaviour, IFireAnimation
    {
        private SpriteRenderer _spriteRenderer;
        public ContentSprite DefaultSprite;
        public float PlaySpeedMultiplier = 1f;

        private void Start()
        {
            _spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        }

        public bool IsPlaying { get; protected set; }

        public abstract void Play(float animSpeed);

        protected float GetAnimationDelay(ContentSprite[] sprites, float animationLength)
        {
            return animationLength / sprites.Length;
        }

        protected IEnumerator Animate(ContentSprite[] sprites, float delay)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                _spriteRenderer.sprite = sprites[i].Get();
                yield return new WaitForSeconds(delay / PlaySpeedMultiplier);
            }
        }

        protected void ResetSprite()
        {
            _spriteRenderer.sprite = DefaultSprite.Get();
        }
    }
}
