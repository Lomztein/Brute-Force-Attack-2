using Lomztein.BFA2.ContentSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class ThirdPartyContentPackRequirement : AchievementRequirement
    {
        public override bool Binary => true;
        public override float Progression => BinaryProgression();
        public override bool RequirementsMet => CheckRequirements();

        private readonly string[] _firstPartyPacks = new[] { "Resources", "Core", "Classic", "Classic+", "Custom" };

        private bool CheckRequirements()
            => ContentManager.Instance.LoadedAndActivePacks.Any(x => !_firstPartyPacks.Contains(x.Name));

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
            base.CheckRequirements();
        }
    }
}
