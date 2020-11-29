﻿using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Research.UniquePrerequisites;
using Lomztein.BFA2.Research.Rewards;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Research
{
    public class ResearchOption
    {
        [ModelProperty]
        public string Name = "Unnamed";
        [ModelProperty]
        public string Description = "Undescd";
        [ModelProperty]
        public ContentSpriteReference Sprite = new ContentSpriteReference();
        [ModelProperty]
        public Color SpriteTint = Color.white;
        [ModelProperty]
        public ResourceCost ResourceCost = new ResourceCost();
        [ModelProperty]
        public int TimeCost;
        public int TimePayed { get; private set; }

        private bool _isCompleted = false;

        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public string[] PrerequisiteIdentifiers = Array.Empty<string>();
        [ModelProperty]
        public string PinIdentifier;

        public float RequirementsProgress => UniquePrerequisites.Sum(x => Mathf.Clamp01(x.Progress)) / UniquePrerequisites.Length;
        public float TimeProgress => UniquePrerequisites.Sum(x => Mathf.Clamp01(x.Progress)) / UniquePrerequisites.Length;

        [ModelProperty]
        public UniquePrerequisite[] UniquePrerequisites = Array.Empty<UniquePrerequisite>();
        public bool UniquePrerequisitesCompleted => _completedRequirements >= UniquePrerequisites.Length;
        private int _completedRequirements;
        [ModelProperty]
        public CompletionReward[] Rewards = Array.Empty<CompletionReward>();


        public event Action<ResearchOption> OnTick;
        public event Action<ResearchOption> OnUniquePrerequisiteProgressed;
        public event Action<ResearchOption> OnUniquePrerequisiteCompleted;
        public event Action<ResearchOption> OnCompleted;

        public void Init()
        {
            foreach (UniquePrerequisite req in UniquePrerequisites)
            {
                req.Init();

                req.OnCompleted += OnRequirementCompleted;
                req.OnProgressed += OnRequirementProgressed;
            }
        }

        private void Stop()
        {
            foreach (UniquePrerequisite req in UniquePrerequisites)
            {
                req.Stop();
            }
        }

        public string[] GetUniqueRequirementsStatuses() => UniquePrerequisites.Select(x => x.Status).ToArray();
        public string[] GetUniqueRequirementsDescriptions() => UniquePrerequisites.Select(x => x.Description).ToArray();
        public string[] GetRewardDescriptions() => Rewards.Select(x => x.Description).ToArray();

        private void OnRequirementProgressed(UniquePrerequisite obj)
        {
            OnUniquePrerequisiteProgressed?.Invoke(this);
        }

        private void OnRequirementCompleted(UniquePrerequisite obj)
        {
            _completedRequirements++;
            obj.Stop();

            if (UniquePrerequisitesCompleted)
            {
                OnUniquePrerequisiteCompleted?.Invoke(this);
            }
        }

        public void BeginResearch ()
        {
            TimePayed = 0;
            if (TimePayed == TimeCost && !_isCompleted)
            {
                CompleteResearch();
            }
        }

        public void Tick ()
        {
            TimePayed++;
            OnTick?.Invoke(this);
            if (TimePayed == TimeCost && !_isCompleted)
            {
                CompleteResearch();
            }
        }

        private void CompleteResearch ()
        {
            Reward();
            Stop();
            Debug.Log("yay!");
            _isCompleted = true;
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