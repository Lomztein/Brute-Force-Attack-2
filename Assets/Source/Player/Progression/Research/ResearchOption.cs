using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Research.UniquePrerequisites;
using Lomztein.BFA2.Research.Rewards;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Research
{
    [CreateAssetMenu(fileName = "NewResearchOption", menuName = "BFA2/Research Option")]
    public class ResearchOption : ScriptableObject
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
        public ResourceCost ResourceCost;
        [ModelProperty]
        public int TimeCost;
        public int TimePayed { get; private set; }

        public bool IsCompleted { get; private set; } = false;

        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public Prerequisite[] Prerequisites;

        [ModelProperty, SerializeReference, SR]
        public UniquePrerequisite[] UniquePrerequisites = Array.Empty<UniquePrerequisite>();
        public bool UniquePrerequisitesCompleted => _completedRequirements >= UniquePrerequisites.Length;
        private int _completedRequirements;
        [ModelProperty, SerializeReference, SR]
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
            if (TimePayed == TimeCost && !IsCompleted)
            {
                CompleteResearch();
            }
        }

        public void Tick ()
        {
            TimePayed++;
            OnTick?.Invoke(this);
            if (TimePayed == TimeCost && !IsCompleted)
            {
                CompleteResearch();
            }
        }

        public void CompleteResearch ()
        {
            Reward();
            Stop();
            IsCompleted = true;
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