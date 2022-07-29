using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class PlayDuringTimeOfDayRequirement : AchievementRequirement
    {
        [ModelProperty]
        public int EarlierstHour;
        [ModelProperty]
        public int LatestHour;

        protected override bool MeetsRequirements()
        {
            return DateTime.Now.Hour >= EarlierstHour && DateTime.Now.Hour <= LatestHour;
        }

        public override void End()
        {
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        public override void Init()
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            CheckRequirements();
        }
    }
}
