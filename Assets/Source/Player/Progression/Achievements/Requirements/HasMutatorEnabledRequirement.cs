using Lomztein.BFA2.Battlefield;
using Lomztein.BFA2.Scenes.Battlefield;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class HasMutatorEnabledRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => BinaryProgression();
        public override bool RequirementsMet => HasApplicableMutatorEnabled();

        [ModelProperty]
        public string[] ApplicableMutatorIdentifiers; // Empty if any.

        private bool HasApplicableMutatorEnabled()
        {
            if (BattlefieldInitializeInfo.InitType == BattlefieldInitializeInfo.InitializeType.New)
            {
                if (ApplicableMutatorIdentifiers.Length == 0 && BattlefieldInitializeInfo.NewSettings.Mutators.Count() > 0)
                {
                    return true;
                }
                if (BattlefieldInitializeInfo.NewSettings.Mutators.Count() > 0 && BattlefieldInitializeInfo.NewSettings.Mutators.All(x => ApplicableMutatorIdentifiers.Contains(x.Identifier)))
                {
                    return true;
                }
            }
            return false;
        }

        public override void End()
        {
            Facade.Battlefield.OnSceneLoaded -= Battlefield_OnSceneLoaded;
        }

        public override void Init()
        {
            Facade.Battlefield.OnSceneLoaded += Battlefield_OnSceneLoaded;
        }

        private void Battlefield_OnSceneLoaded()
        {
            CheckRequirements();
        }
    }
}
