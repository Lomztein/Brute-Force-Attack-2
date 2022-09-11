using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class PlayerDeathRequirement : AchievementRequirement
    {
        private TimedFlag _flag = new TimedFlag();
        [ModelProperty]
        public float FlagTimeout;

        protected override bool MeetsRequirements()
            => _flag.Get();

        public override void End()
        {
            Facade.Battlefield.Player.OnHealthExhausted -= Player_OnHealthExhausted;
        }

        public override void Init()
        {
            _flag.Timeout = FlagTimeout;
            Facade.Battlefield.Player.OnHealthExhausted += Player_OnHealthExhausted;
        }

        private void Player_OnHealthExhausted(object source)
        {
            _flag.Mark();
            CheckRequirements();
        }
    }
}
