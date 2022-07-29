using Lomztein.BFA2.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class SaveMapRequirement : AchievementRequirement
    {
        protected override bool MeetsRequirements()
        {
            return _saveFlag.Get();
        }

        private TimedFlag _saveFlag = new TimedFlag();

        public override void End()
        {
            Facade.MapEditor.OnMapSaved += OnMapSaved;
        }

        public override void Init()
        {
            Facade.MapEditor.OnMapSaved += OnMapSaved;
        }

        private void OnMapSaved(MapData data, string path)
        {
            _saveFlag.Mark();
            CheckRequirements();
        }
    }
}
