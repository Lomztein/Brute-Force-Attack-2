using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Player.Progression.Achievements.Requirements;
using Lomztein.BFA2.Player.Progression.Achievements.Rewards;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    [CreateAssetMenu(fileName = "NewAchievement", menuName = "BFA2/Achievement")]
    public class Achievement : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Identifier;
        [ModelProperty] [TextArea]
        public string Description;
        [ModelProperty]
        [TextArea]
        public string RewardDescription;
        [ModelProperty]
        [TextArea]
        public string FunFact;
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public bool Hidden;

        [ModelProperty, SerializeReference, SR]
        public IAchievementRequirement Requirement;
        [ModelProperty, SerializeReference, SR]
        public IAchievementReward Reward;

        private bool _completed;

        public event Action<Achievement> OnCompleted;
        public event Action<Achievement> OnProgressed;

        public void Init ()
        {
            Requirement.Init(OnRequirementProgressed);
        }

        public void End ()
        {
            Requirement.End();
        }

        public void Complete ()
        {
            if (!_completed)
            {
                if (Reward != null)
                {
                    Reward.Apply();
                }
                OnCompleted?.Invoke(this);
                _completed = true;
            }
        }

        private void OnRequirementProgressed()
        {
            OnProgressed?.Invoke(this);
            if (Requirement.RequirementsMet)
            {
                Complete();
            }
        }

        public ValueModel SerializeProgress() => Requirement.SerializeProgress();
        public void DeserializeProgress(ValueModel source)
        {
            if (!ValueModel.IsNull(source))
            {
                Requirement?.DeserializeProgress(source);
            }
        }
    }
}
