using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Filters;
using Lomztein.BFA2.Serialization;
using Lomztein.BFA2.Serialization.Models;
using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class StructureEventExecutionCountRequirement : AchievementRequirement
    {
        [ModelAssetReference]
        public EventInfo ListenTo;
        [ModelProperty]
        public bool IncludeAssemblyComponents;
        [ModelProperty, SerializeReference, SR]
        public IModdableFilter[] CheckAgainst;
        [ModelProperty]
        public int RequiredCount;
        [ModelProperty]
        public bool AllowGreater;
        [ModelProperty]
        public bool AllowLesser;
        [ModelProperty]
        public bool CrossGames;

        protected override bool MeetsRequirements()
        {
            return _count == RequiredCount ||
                (AllowGreater && _count > RequiredCount) ||
                (AllowLesser && _count < RequiredCount);
        }

        int _count;

        public override ValueModel SerializeProgress()
        {
            return new PrimitiveModel(_count);
        }

        public override void DeserializeProgress(ValueModel source)
        {
            _count = (source as PrimitiveModel).ToObject<int>();
        }

        public override void End()
        {
            Facade.Battlefield.Structures.OnStructureEventExecuted -= Structures_OnStructureEventExecuted;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneUnloaded -= Battlefield_OnSceneUnloaded;
            }
        }

        public override void Init()
        {
            Facade.Battlefield.Structures.OnStructureEventExecuted += Structures_OnStructureEventExecuted;
            if (!CrossGames)
            {
                Facade.Battlefield.OnSceneUnloaded += Battlefield_OnSceneUnloaded;
            }
        }

        private void Battlefield_OnSceneUnloaded()
        {
            _count = 0;
        }

        private void Structures_OnStructureEventExecuted(Structure arg1, IEvent arg2, object arg3)
        {
            if (CheckAgainst.All(x => x.Check(arg1)) && ListenTo.Identifier == arg2.Identifier)
            {
                _count++;
                CheckRequirements();
            }
        }
    }
}
