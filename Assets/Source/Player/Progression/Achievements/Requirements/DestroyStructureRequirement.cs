using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class DestroyStructureRequirement : AchievementRequirement
    {
        private TimedFlag _flag = new TimedFlag();
        [ModelProperty, SerializeReference, SR]
        public IModdableFilter[] Filter;

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureRemoved -= Structures_OnStructureRemoved;
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureRemoved -= Structures_OnStructureRemoved;
        }

        private void Structures_OnStructureRemoved(Structures.Structure obj)
        {
            if (!Filter.Any() || Filter.All(x => x.Check(obj))) {
                _flag.Mark();
                CheckRequirements();
            }
        }
    }
}
