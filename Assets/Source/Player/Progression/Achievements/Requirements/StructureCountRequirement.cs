using Lomztein.BFA2.Structures.StructureManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class StructureCountRequirement : AchievementRequirement
    {
        public int RequiredCount;
        public bool AllowGreater;
        public bool AllowLess;

        protected override bool MeetsRequirements ()
        {
            int count = StructureManager.GetStructures().Length;
            return count == RequiredCount || 
                (AllowGreater && count > RequiredCount) ||
                (AllowLess && count < RequiredCount);
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureAdded -= Structures_OnStructureAdded;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureAdded += Structures_OnStructureAdded;
        }

        private void Structures_OnStructureAdded(Structures.Structure arg1, object arg2)
        {
            CheckRequirements();
        }
    }
}
