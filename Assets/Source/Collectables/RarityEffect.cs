using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Collectables
{
    // TODO: Somehow extend to allow unique effects for different rarities.
    public class RarityEffect : MonoBehaviour
    {
        public const string CONTENT_PATH = "*/Rarities/Effects/*";

        [ModelAssetReference]
        public Rarity Rarity;
        [ModelProperty]
        public float SpriteRotationRate;
        [ModelProperty]
        public float SpriteBobRate;
        [ModelProperty]
        public float SpriteBobAmount;
        [ModelProperty]
        public float ParticleLife;
        [ModelProperty]
        public float ParticleRate;

        private ParticleSystem _ambientParticle;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _rendererDefaultScale;
        private float _rendererScaleFactor;

        private float _scale = 1f;

        private void Awake()
        {
            _ambientParticle = GetComponentInChildren<ParticleSystem>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _rendererScaleFactor = _spriteRenderer.bounds.size.magnitude;
            _rendererDefaultScale = _spriteRenderer.transform.localScale / _rendererScaleFactor;
            _spriteRenderer.transform.localScale = _rendererDefaultScale;
        }

        private void FixedUpdate()
        {
            _spriteRenderer.transform.Rotate(0f, 0f, SpriteRotationRate * Time.fixedDeltaTime);
            _spriteRenderer.transform.localScale = (Vector2.one * SpriteBobAmount * Mathf.Sin(Time.time * Mathf.PI * 2f * SpriteBobRate) / _rendererScaleFactor) + _rendererDefaultScale * _scale; // idk
        }

        public void Assign (Rarity rarity, Sprite sprite, Color spriteTint)
        {
            ParticleSystem.MainModule main = _ambientParticle.main;
            main.startColor = rarity.Color;
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = spriteTint;
        }

        public void Scale (float scalar)
        {
            _scale = scalar;
            ParticleSystem.EmissionModule emission = _ambientParticle.emission;
            emission.rateOverTimeMultiplier = ParticleRate * _scale;
        }

        public void DetachAndStop ()
        {
            transform.SetParent(null);
            Destroy(gameObject, ParticleLife);
        }

        public static GameObject Instantiate (Rarity rarity)
        {
            IContentCachedPrefab[] all = Content.GetAll<IContentCachedPrefab>(CONTENT_PATH).ToArray();
            IContentCachedPrefab defaultEffect = all.First(x => x.GetCache().GetComponent<RarityEffect>().Rarity == null);
            IContentCachedPrefab fit = all.FirstOrDefault(x => x.GetCache().GetComponent<RarityEffect>().Rarity?.Identifier == rarity.Identifier);
            return (fit ?? defaultEffect).Instantiate();
        }
    }
}
