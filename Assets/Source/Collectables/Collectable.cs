using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        [ModelProperty]
        public float CollectionTime;
        [ModelAssetReference]
        public Rarity Rarity;
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public Color SpriteTint = Color.white;

        public RarityEffect Effect { get; private set; }
        private float _collectionProgress;
        private bool _isCollected;

        public event Action<Collectable> OnCollected;

        protected virtual void Start()
        {
            CreateRarityEffect();
        }

        private void CreateRarityEffect ()
        {
            Effect = RarityEffect.Instantiate(Rarity).GetComponent<RarityEffect>();
            Effect.Assign(Rarity, Sprite.Get(), SpriteTint);
            Effect.transform.SetParent(transform);
            Effect.transform.position = transform.position;
        }

        public bool TickCollection (float deltaTime, float collectionRate)
        {
            if (!_isCollected)
            {
                _collectionProgress += deltaTime * collectionRate;
                Effect.Scale(1 - (_collectionProgress / CollectionTime));

                if (_collectionProgress >= CollectionTime)
                {
                    DoCollect();
                    return true;
                }
            }
            return false;
        }

        public bool InstantCollection ()
        {
            if (!_isCollected)
            {
                DoCollect();
                return true;
            }
            return false;
        }

        private void DoCollect ()
        {
            Collect();
            _isCollected = true;
            OnCollected?.Invoke(this);
            Effect.DetachAndStop();
            Destroy(gameObject);
        }

        protected abstract void Collect();
    }
}
