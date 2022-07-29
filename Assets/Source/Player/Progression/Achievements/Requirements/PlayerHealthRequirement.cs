using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class PlayerHealthRequirement : AchievementRequirement
    {
        [ModelProperty]
        public float Threshold;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;

        protected override bool MeetsRequirements()
        {
            float health = Player.Health.GetCurrentHealth();
            return health == Threshold ||
                (AllowGreater && health > Threshold) ||
                (AllowLesser && health < Threshold);
        }

        public override void End()
        {
            Facade.Battlefield.Player.OnHealthChanged -= Player_OnHealthChanged;
        }

        public override void Init()
        {
            Facade.Battlefield.Player.OnHealthChanged += Player_OnHealthChanged;
        }

        private void Player_OnHealthChanged(float arg1, float arg2, float arg3)
        {
            CheckRequirements();
        }
    }
}
