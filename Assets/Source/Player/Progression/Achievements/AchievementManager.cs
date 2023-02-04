using Lomztein.BFA2.ContentSystem;
using Lomztein.BFA2.Player.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lomztein.BFA2.Player.Progression.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance;

        private const string ACHIEVEMENTS_PATH = "*/Achievements/*";
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

        private void OnDestroy()
        {
            CleanUp();
            ProfileManager.SaveCurrent();
        }

        private Achievement[] LoadAchievements ()
        {
            return Content.GetAll<Achievement>(ACHIEVEMENTS_PATH).ToArray();
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
                    achievement.DeserializeProgress (ProfileManager.CurrentProfile.GetAchievementStatus(achievement.Identifier).Progression);

                    achievement.Init();
                    achievement.OnCompleted += AchievementCompleted;
                    achievement.OnProgressed += AchievementProgressed;
                }
            }
        }

        private void CleanUp ()
        {
            foreach (Achievement achievement in Achievements)
            {
                if (!IsCompleted(achievement, ProfileManager.CurrentProfile))
                {
                    achievement.End();
                    achievement.OnCompleted -= AchievementCompleted;
                    achievement.OnProgressed -= AchievementProgressed;
                }
            }
        }

        private void AchievementProgressed(Achievement obj)
        {
            ProfileManager.CurrentProfile.MutateAchievementStatus(obj.Identifier, x => x.Progression = obj.SerializeProgress());
        }

        private void AchievementCompleted(Achievement obj)
        {
            obj.End();
            obj.OnCompleted -= AchievementCompleted;
            obj.OnProgressed -= AchievementProgressed;

            ProfileManager.CurrentProfile.MutateAchievementStatus(obj.Identifier, x => { x.IsCompleted = true; x.CompletedOn = DateTime.Now; });
            ProfileManager.SaveCurrent();

            OnAchievementCompleted?.Invoke(obj);
        }

        private bool IsCompleted(Achievement achievement, PlayerProfile profile)
            => profile.GetAchievementStatus(achievement.Identifier).IsCompleted;
    }
}
