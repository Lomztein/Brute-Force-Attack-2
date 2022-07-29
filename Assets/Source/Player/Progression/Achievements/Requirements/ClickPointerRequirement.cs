using Lomztein.BFA2;
using Lomztein.BFA2.Player.Progression.Achievements.Requirements;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ClickPointerRequirement : AchievementRequirement
    {
        public override bool Binary => false;
        public override float Progression => _clicks / (float)TargetClicks;
        public override bool RequirementsMet => _clicks >= TargetClicks;

        private int _clicks;
        [ModelProperty]
        public int TargetClicks;

        public override void DeserializeProgress(ValueModel source)
        {
            _clicks = (source as PrimitiveModel).ToObject<int>();
        }

        public override void End()
        {
            Input.PrimaryClickStarted -= ClickStarted;
            Input.SecondaryClicKStarted -= ClickStarted;
        }

        public override void Init()
        {
            Input.PrimaryClickStarted += ClickStarted;
            Input.SecondaryClicKStarted += ClickStarted;
        }

        private void ClickStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _clicks++;
            CheckRequirements();
        }

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_clicks);
        }
    }
}