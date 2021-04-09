using Lomztein.BFA2.ContentSystem.References;
using Lomztein.BFA2.Player.Progression.Achievements.Requirements;
using Lomztein.BFA2.Serialization;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    [CreateAssetMenu(fileName = "NewAchievement", menuName = "BFA2/Achievement")]
    public class Achievement : SerializedScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;
        [ModelProperty]
        public string Identifier;
        [ModelProperty]
        public ContentSpriteReference Sprite;
        [ModelProperty]
        public bool Hidden;

        [ModelProperty]
        public IAchievementRequirement Requirement;
        [ModelProperty]
        public IAchievementReward Reward;

        private bool _completed;

        public event Action<Achievement> OnCompleted;

        public void Init (Facade facade)
        {
            Requirement.Init(facade, OnRequirementCompleted);
        }

        public void End (Facade facade)
        {
            Requirement.End(facade);
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
    }
}
