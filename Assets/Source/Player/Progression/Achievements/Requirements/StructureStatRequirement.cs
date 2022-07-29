using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class StructureStatRequirement : AchievementRequirement
    {
        [ModelAssetReference]
        public StatInfo StatInfo;
        [ModelProperty]
        public IModdableFilter[] CheckAgainst;
        [ModelProperty]
        public float Threshold;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;

        private bool _thresholdReached;

        protected override bool MeetsRequirements()
        {
            return _thresholdReached;
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureStatChanged -= Structures_OnStructureStatChanged;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureStatChanged += Structures_OnStructureStatChanged;
        }

        private void Structures_OnStructureStatChanged(Structures.Structure arg1, IStatReference arg2, object arg3)
        {
            if (arg2.Identifier == StatInfo.Identifier && (!CheckAgainst.Any() || CheckAgainst.All(x => x.Check(arg1))))
            {
                float value = arg2.GetValue();
                if (value == Threshold ||
                        (AllowGreater && value > Threshold) ||
                        (AllowLesser && value < Threshold))
                {
                    _thresholdReached = true;
                    CheckRequirements();
                }
            }
        }
    }
}
