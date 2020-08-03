using Lomztein.BFA2.Content.References;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Research.Requirements;
using Lomztein.BFA2.Research.Rewards;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Research
{
    public class ResearchOption : MonoBehaviour
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public ResourceCost InitialCost = new ResourceCost();

        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string[] PrerequisiteIdentifiers;

        public float Progress => _requirements.Sum(x => Mathf.Clamp01(x.Progress)) / _requirements.Length;

        private CompletionRequirement[] _requirements;
        private CompletionReward[] _rewards;

        private int _completedRequirements;

        public event Action<ResearchOption> OnProgressed;
        public event Action<ResearchOption> OnCompleted;

        public void Init()
        {
            _requirements = GetComponents<CompletionRequirement>();
            _rewards = GetComponents<CompletionReward>();

            foreach (CompletionRequirement req in _requirements)
            {
                req.Init();

                req.OnCompleted += OnRequirementCompleted;
                req.OnProgressed += OnRequirementProgressed;
            }
        }

        public void Stop()
        {
            foreach (CompletionRequirement req in _requirements)
            {
                req.Stop();
            }
        }

        private void OnRequirementProgressed(CompletionRequirement obj)
        {
            OnProgressed?.Invoke(this);
        }

        private void OnRequirementCompleted(CompletionRequirement obj)
        {
            _completedRequirements++;
            if (_completedRequirements == _requirements.Length)
            {
                Reward();
                Stop();
                OnCompleted?.Invoke(this);
            }
        }

        private void Reward ()
        {
            foreach (var reward in _rewards)
            {
                reward.ApplyReward();
            }
        }
    }
}