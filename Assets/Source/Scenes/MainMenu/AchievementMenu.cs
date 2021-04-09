using Lomztein.BFA2.Player.Profile;
using Lomztein.BFA2.Player.Progression.Achievements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class AchievementMenu : MonoBehaviour
    {
        public Transform CellParent;
        public GameObject CellPrefab;

        private void GenerateCells ()
        {
            foreach (Transform child in CellParent)
            {
                Destroy(child.gameObject);
            }

            Achievement[] all = AchievementManager.Instance.Achievements;
            foreach (Achievement achievement in all)
            {
                GameObject newCell = Instantiate(CellPrefab, CellParent);
                newCell.GetComponent<AchievementMenuCell>().Assign(achievement, ProfileManager.CurrentProfile.HasCompletedAchievement(achievement.Identifier));
            }

            (transform as RectTransform).sizeDelta += Vector2.up;
        }

        void Start()
        {
            GenerateCells();
            AchievementManager.Instance.OnAchievementCompleted += OnAchievementCompleted;
        }

        private void OnDestroy()
        {
            AchievementManager.Instance.OnAchievementCompleted -= OnAchievementCompleted;
        }

        private void OnAchievementCompleted(Achievement obj)
        {
            GenerateCells();
        }
    }
}
