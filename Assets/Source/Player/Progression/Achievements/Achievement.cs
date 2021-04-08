using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    public class Achievement : ScriptableObject
    {
        [ModelProperty]
        public string Name;
        [ModelProperty]
        public string Description;

        [ModelProperty]
        public IAchievementRequirement[] Requirements;

        [ModelProperty]
        public IAchievementReward[] Rewards;

        public void Start ()
        {
            foreach (IAchievementRequirement requirement in Requirements)
            {
                requirement.Start(() => OnRequirementCompleted(requirement));
            }
        }

        public void Stop ()
        {
            foreach (IAchievementRequirement requirement in Requirements)
            {
                requirement.Stop();
            }
        }

        private void OnRequirementCompleted(IAchievementRequirement requirement)
        {

        }

        private void Reward ()
        {

        }
    }
}
