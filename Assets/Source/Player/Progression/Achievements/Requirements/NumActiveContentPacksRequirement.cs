using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class NumActiveContentPacksRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => BinaryProgression();
        public override bool RequirementsMet =>
            ContentManager.Instance.LoadedAndActivePacks.Count() == RequiredAmount ||
            (AllowGreaterThan && ContentManager.Instance.LoadedAndActivePacks.Count() > RequiredAmount);

        [ModelProperty]
        public int RequiredAmount;
        [ModelProperty]
        public bool AllowGreaterThan;

        public override void End()
        {
            ContentManager.OnPostContentReload -= ContentManager_OnPostContentReload;
        }

        public override void Init()
        {
            ContentManager.OnPostContentReload += ContentManager_OnPostContentReload;
        }

        private void ContentManager_OnPostContentReload(IEnumerable<IContentPack> obj)
        {
            CheckRequirements();
        }
    }
}
