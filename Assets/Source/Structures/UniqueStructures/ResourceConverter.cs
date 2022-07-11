using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Enemies;
using Lomztein.BFA2.Enemies.Waves;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.UniqueStructures
{
    // Lots of if-branching in this class. May be difficult to follow.

    public class ResourceConverter : Structure
    {
        public StatInfo ConversionSpeedInfo;
        public StatInfo ConversionTargetInfo;

        public Resource ConsumingResource;
        public Resource ProducingResource;

        public bool Paused;
        public bool Automatic;

        private bool _isConverting;

        public float BaseConversionTarget;
        public float BaseConversionSpeed;
        private IStatReference _conversionTarget;
        private IStatReference _conversionSpeed;

        private float _conversionProgress;

        private IResourceContainer _resourceContainer;

        public ContentSpriteSheet ConversionSpriteSheet;
        public ContentSpriteSheet IdleSpriteSheet;

        private SpriteRenderer _conversionSprite;
        private SpriteRenderer _backgroundSprite;

        public Color IdleColor;
        public Color NotEnoughResourcesColor;

        private LooseDependancy<RoundController> _roundController = new LooseDependancy<RoundController>();

        private void Start()
        {
            _conversionSpeed = Stats.AddStat(ConversionSpeedInfo, BaseConversionSpeed, this);
            _conversionTarget = Stats.AddStat(ConversionTargetInfo, BaseConversionTarget, this);
            _resourceContainer = GetComponent<IResourceContainer>();

            _roundController.IfExists(x =>
            {
                x.OnWaveStarted += OnWaveStarted;
                x.OnWaveFinished += OnWaveFinished;
                SetPause(x.State != RoundController.RoundState.InProgress);
            });
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            _roundController.IfExists(x =>
            {
                x.OnWaveStarted -= OnWaveStarted;
                x.OnWaveFinished -= OnWaveFinished;
            });
        }

        private void OnWaveFinished(int arg1, WaveHandler arg2)
        {
            SetPause(true);
        }

        private void OnWaveStarted(int arg1, WaveHandler arg2)
        {
            SetPause(false);
        }

        private SpriteRenderer GetConversionSprite ()
        {
            if (_backgroundSprite == null)
            {
                _backgroundSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            }
            return _backgroundSprite;
        }

        private SpriteRenderer GetBackgroundSprite ()
        {
            if (_conversionSprite == null)
            {
                _conversionSprite = transform.Find("Conversion").GetComponent<SpriteRenderer>();
            }
            return _conversionSprite;
        }

        private void FixedUpdate()
        {
            if (Automatic && !_isConverting && !Paused)
            {
                TryStartConversion();
            }

            if (_isConverting)
            {
                UpdateConverting(Time.fixedDeltaTime);
            }
            else
            {
                UpdateIdle(Time.fixedDeltaTime);
            }
        }

        private void UpdateConverting(float fixedDeltaTime)
        {
            if (!Paused)
            {
                _conversionProgress += _conversionSpeed.GetValue() * fixedDeltaTime;
                _conversionSprite.sprite = ConversionSpriteSheet.GetSprite(_conversionProgress / _conversionTarget.GetValue());
                if (_conversionProgress >= _conversionTarget.GetValue())
                {
                    CompleteConversion();
                }
            }
        }

        private void UpdateIdle(float fixedDeltaTime)
        {
            GetBackgroundSprite().sprite = IdleSpriteSheet.GetSprite(Mathf.Abs(Mathf.Sin(Time.time)));
        }

        public bool TryStartConversion ()
        {
            if (!_isConverting && TryClaimResource())
            {
                _isConverting = true;
                _conversionProgress = 0f;

                GetBackgroundSprite().color = ConsumingResource.Color;
                GetConversionSprite().color = ProducingResource.Color;

                return true;
            }
            else
            {
                GetBackgroundSprite().color = NotEnoughResourcesColor;
            }
            return false;
        }

        private void CompleteConversion ()
        {
            if (_isConverting)
            {
                _isConverting = false;
                AddConvertedResource();
            }
        }

        public void CancelConversion ()
        {
            if (_isConverting)
            {
                _isConverting = false;
                RefundResource();
            }
        }

        public void SetPause (bool pause)
        {
            Paused = pause;
        }

        private bool TryClaimResource()
            => _resourceContainer.TrySpend(new SingleResourceCost(ConsumingResource, ConsumingResource.BinaryValue));

        private void RefundResource ()
            => _resourceContainer.AddResources(new SingleResourceCost(ConsumingResource, ConsumingResource.BinaryValue));


        private void AddConvertedResource ()
            => _resourceContainer.AddResources(new SingleResourceCost(ProducingResource, ProducingResource.BinaryValue));
    }
}
