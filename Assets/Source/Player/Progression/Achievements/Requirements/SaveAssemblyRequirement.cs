using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SaveAssemblyRequirement : AchievementRequirement
    {
        protected override bool MeetsRequirements()
        {
            return _saveFlag.Get();
        }

        private TimedFlag _saveFlag = new TimedFlag();

        public override void End()
        {
            Facade.AssemblyEditor.OnAssemblySaved += AssemblyEditor_OnAssemblySaved;
        }

        public override void Init()
        {
            Facade.AssemblyEditor.OnAssemblySaved += AssemblyEditor_OnAssemblySaved;
        }

        private void AssemblyEditor_OnAssemblySaved(Structures.Turrets.TurretAssembly arg1, string arg2)
        {
            _saveFlag.Mark();
            CheckRequirements();
        }
    }
}
