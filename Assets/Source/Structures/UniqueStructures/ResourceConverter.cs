using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.UniqueStructures
{
    public class ResourceConverter : Structure
    {
        [ModelProperty]
        public ContentSpriteSheet ProgressSprites = new ContentSpriteSheet();
        [ModelProperty]
        public ContentSpriteSheet IdleSprites = new ContentSpriteSheet();
        [ModelProperty]
        public Resource ConsumingResource { get; private set; }
        [ModelProperty]
        public Resource ProducingResource { get; private set; }

        private bool _producing;
        private bool _halted;
        private bool _autoProduce;

        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _backgroundSpriteRenderer;
        private IResourceContainer _resourceContainer;
        private Color _idleColor = Color.red;

        [ModelProperty]
        public float RequiredProcessing;
        public IStatReference ProcessingSpeed;
        private float _currentProcessing;

        [ModelProperty]
        public float IdleAnimationSpeed;
        private float _idleAnimationProgress;

        // These three dictionaries may be better defined somewhere else, or as a ModelProperty.
        // Would need to implement Dictionary support for model properties first.
        private Dictionary<Resource, int> _resouceCosts = new Dictionary<Resource, int>()
        {
            { Resource.Credits, 250 },
            { Resource.Research, 1 },
            { Resource.Binaries, 1 },
        };

        private Dictionary<Resource, int> _resourceValues = new Dictionary<Resource, int>()
        {
            { Resource.Credits, 250 },
            { Resource.Research, 1 },
            { Resource.Binaries, 1 },
        };

        private Dictionary<Resource, Color> _resourceColors = new Dictionary<Resource, Color>()
        {
            { Resource.Credits, Color.green },
            { Resource.Research, Color.blue },
            { Resource.Binaries, Color.magenta },
        };
        private void Start()
        {
            _spriteRenderer = transform.Find("Progress").GetComponent<SpriteRenderer>();
            _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
            _resourceContainer = GetComponent<IResourceContainer>();
            ProcessingSpeed = Stats.AddStat("ProcessingSpeed", "ProcessingSpeed", "The speed of which this device converts resources", 1);

            SetConsumingResource(ConsumingResource);
            SetProducingResource(ProducingResource);

            StartProduction();
        }

        public void CancelProduction ()
        {
            if (_producing)
            {
                ReturnResource();
                _producing = false;
                _idleAnimationProgress = 0f;
            }
        }

        public void StartProduction ()
        {
            if (!_producing && TryClaimResource())
            {
                _producing = true;
                _currentProcessing = 0f;
            }
        }

        private void Process ()
        {
            _currentProcessing += ProcessingSpeed.GetValue();

            if (_currentProcessing >= RequiredProcessing) ;
            {
                _currentProcessing = 0f;
                Convert();
            }

            _spriteRenderer.sprite = IdleSprites.GetSprite(_idleAnimationProgress);
        }

        private void Convert ()
        {
            _resourceContainer.AddResources(new SingleResourceCost(ProducingResource, _resourceValues[ConsumingResource]));
        }

        private void Idle ()
        {
            _idleAnimationProgress += IdleAnimationSpeed * Time.fixedDeltaTime;
            if (_idleAnimationProgress >= 1f)
            {
                _idleAnimationProgress = 0f;
            }

            _spriteRenderer.color = _idleColor;
            _spriteRenderer.sprite = IdleSprites.GetSprite(_idleAnimationProgress);
        }

        private void FixedUpdate()
        {
            if (_producing)
            {
                Process();
            }
            else
            {
                Idle();
            }
        }

        public void SetConsumingResource(Resource resource)
        {
            _backgroundSpriteRenderer.sprite = IdleSprites.GetSprite(0);
            _backgroundSpriteRenderer.color = _resourceColors[resource];
            ConsumingResource = resource;
        }

        public void SetProducingResource (Resource resource)
        {
        }

        private bool TryClaimResource()
            => _resourceContainer.TrySpend(new SingleResourceCost (ConsumingResource, _resouceCosts[ConsumingResource]));

        private void ReturnResource()
            => _resourceContainer.AddResources(new SingleResourceCost(ConsumingResource, _resouceCosts[ConsumingResource]));
    }
}
