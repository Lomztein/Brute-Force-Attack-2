using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    public class UnloadSceneRequirement : AchievementRequirement
    {
        private bool _sceneUnloaded;
        public int SceneBuildIndex;
        public string SceneName;

        public override bool RequirementsMet => _sceneUnloaded;

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
                    _sceneUnloaded = true;
                    CheckRequirements();
                }
            }
            else
            {
                if (arg0.name == SceneName)
                {
                    _sceneUnloaded = true;
                }
            }
        }
    }
}
