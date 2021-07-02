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
        [ModelProperty] [TextArea]
        public string Description;
        [ModelProperty]
        public string Identifier;
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
            Requirement.Init(OnRequirementCompleted, OnRequirementProgressed);
        }

        public void End ()
        {
            Requirement.End();
        }

        public void Complete ()
        {
            if (!_completed)
            {
                Reward?.Apply();
                OnCompleted?.Invoke(this);
                _completed = true;
            }
        }

        private void OnRequirementCompleted()
        {
            Complete();
        }

        private void OnRequirementProgressed()
        {
            OnProgressed?.Invoke(this);
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
