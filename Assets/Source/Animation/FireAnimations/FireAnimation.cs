﻿using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Serialization;
using System.Collections;
using UnityEngine;

namespace Lomztein.BFA2.Animation.FireAnimations
{
    public abstract class FireAnimation : MonoBehaviour, IFireAnimation
    {
        private SpriteRenderer _spriteRenderer;
        [ModelProperty]
        public ContentSprite DefaultSprite;
        [ModelProperty]
        public float PlaySpeedMultiplier = 1f;

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
                GetSpriteRenderer().sprite = sprites[i].Get();
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
            _spriteRenderer.sprite = DefaultSprite.Get();
        }
    }
}
