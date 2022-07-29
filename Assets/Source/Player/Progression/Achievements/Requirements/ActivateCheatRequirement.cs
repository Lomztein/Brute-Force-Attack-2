using Lomztein.BFA2.Cheats;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ActivateCheatRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => BinaryProgression();
        public override bool RequirementsMet => _cheatActivated;

        private bool _cheatActivated;
        [ModelProperty]
        public string[] ApplicableCodes;

        public override void End()
        {
            CheatCode.OnCheatActivated -= CheatCode_OnCheatActivated;
        }

        public override void Init()
        {
            CheatCode.OnCheatActivated += CheatCode_OnCheatActivated;
        }

        private void CheatCode_OnCheatActivated(CheatCode arg1, string arg2)
        {
            if (!ApplicableCodes.Any() || ApplicableCodes.Any(x => arg1.Code.StartsWith(x)))
            {
                _cheatActivated = true;
                CheckRequirements();
            }
        }
    }
}
