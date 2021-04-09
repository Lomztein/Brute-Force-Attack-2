using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Player.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance;

        private const string ACHIEVEMENTS_PATH = "*/Achievements";
        public Achievement[] Achievements;

        public event Action<Achievement> OnAchievementCompleted;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Achievements = LoadAchievements();
            InitAchievements();
        }

        private Achievement[] LoadAchievements ()
        {
            return Content.GetAll<Achievement>(ACHIEVEMENTS_PATH);
        }

        private void InitAchievements()
        {
            foreach (Achievement achievement in Achievements)
            {
                if (IsCompleted(achievement, ProfileManager.CurrentProfile))
                {
                    achievement.Complete();
                }
                else
                {
                    achievement.Init(Facade.GetInstance());
                    achievement.OnCompleted += AchievementCompleted;
                }
            }
        }

        private void AchievementCompleted(Achievement obj)
        {
            obj.End(Facade.GetInstance());
            obj.OnCompleted -= AchievementCompleted;
            ProfileManager.CurrentProfile.AddCompletedAchievement(obj.Identifier);
            ProfileManager.SaveCurrent();
            OnAchievementCompleted?.Invoke(obj);
        }

        private bool IsCompleted(Achievement achievement, PlayerProfile profile)
            => profile.HasCompletedAchievement(achievement.Identifier);
    }
}
