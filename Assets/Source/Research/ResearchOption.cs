using Lomztein.BFA2.ContentSystem.References;
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
        public string Name = "Unnamed";
        [ModelProperty]
        public string Description = "Undescd";
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public Color SpriteTint = Color.white;
        [ModelProperty]
        public float Weight = 1;
        [ModelProperty]
        public ResourceCost InitialCost = new ResourceCost();

        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string[] PrerequisiteIdentifiers;

        public float Progress => Requirements.Sum(x => Mathf.Clamp01(x.Progress)) / Requirements.Length;

        private CompletionRequirement[] Requirements => GetComponents<CompletionRequirement>();
        private CompletionReward[] Rewards => GetComponents<CompletionReward>();

        private int _completedRequirements;

        public event Action<ResearchOption> OnProgressed;
        public event Action<ResearchOption> OnCompleted;

        public void Init()
        {
            CompletionRequirement[] requirements = GetComponents<CompletionRequirement>();
            CompletionReward[] rewards = GetComponents<CompletionReward>();

            if (requirements.Length == 0)
            {
                CompleteResearch();
            }
            else
            {
                foreach (CompletionRequirement req in requirements)
                {
                    req.Init();

                    req.OnCompleted += OnRequirementCompleted;
                    req.OnProgressed += OnRequirementProgressed;
                }
            }
        }

        public string[] GetRequirementStatuses() => Requirements.Select(x => x.Status).ToArray();
        public string[] GetRequirementDescriptions() => Requirements.Select(x => x.Description).ToArray();
        public string[] GetRewardDescriptions() => Rewards.Select(x => x.Description).ToArray();

        public void Stop()
        {
            foreach (CompletionRequirement req in Requirements)
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
            if (_completedRequirements == Requirements.Length)
            {
                CompleteResearch();
            }
        }

        private void CompleteResearch ()
        {
            Reward();
            Stop();
            OnCompleted?.Invoke(this);
        }

        private void Reward ()
        {
            foreach (var reward in Rewards)
            {
                reward.ApplyReward();
            }
        }
    }
}