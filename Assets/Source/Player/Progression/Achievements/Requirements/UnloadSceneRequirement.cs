using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class UnloadSceneRequirement : AchievementRequirement
    {
        public int SceneBuildIndex;
        public string SceneName;

        private TimedFlag _flag = new TimedFlag(0.1f);
        public override bool RequirementsMet => _flag.Get();

        public override void End()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public override void Init()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene arg0)
        {
            if (string.IsNullOrEmpty(SceneName))
            {
                if (arg0.buildIndex == SceneBuildIndex)
                {
                    _flag.Mark();
                    CheckRequirements();
                }
            }
            else
            {
                if (arg0.name == SceneName)
                {
                    _flag.Mark();
                    CheckRequirements();
                }
            }
        }
    }
}
