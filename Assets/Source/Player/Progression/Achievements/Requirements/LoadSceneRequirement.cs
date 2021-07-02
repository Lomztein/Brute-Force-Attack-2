using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Progression.Achievements.Requirements
{
    [System.Serializable]
    public class LoadSceneRequirement : AchievementRequirement
    {
        private bool _sceneLoaded;
        public int SceneBuildIndex;
        public string SceneName;

        public override bool Binary => true;
        public override float Progression => Completed ? 1f : 0f;
        public override bool Completed => _sceneLoaded;

        public override void End()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public override void Init()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (string.IsNullOrEmpty(SceneName))
            {
                if (arg0.buildIndex == SceneBuildIndex)
                {
                    _sceneLoaded = true;
                    _onCompletedCallback();
                }
            }
            else
            {
                if (arg0.name == SceneName)
                {
                    _sceneLoaded = true;
                    _onCompletedCallback();
                }
            }
        }
    }
}
