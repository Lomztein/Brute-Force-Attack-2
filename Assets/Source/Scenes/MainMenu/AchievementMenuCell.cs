using Lomztein.BFA2.LocalizationSystem;
using Lomztein.BFA2.Player.Profile;
using Lomztein.BFA2.Player.Progression.Achievements;
using Lomztein.BFA2.UI.ToolTip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class AchievementMenuCell : MonoBehaviour, IHasToolTip
    {
        public Image Image;
        public Sprite LockedSprite;
        public GameObject ToolTipPrefab;

        private Achievement _achievement;
        private bool _isAchievementUnlocked;

        public void Assign (Achievement achievement, bool unlocked)
        {
            _achievement = achievement;
            _isAchievementUnlocked = unlocked;
            if (unlocked)
            {
                Image.sprite = _achievement.Sprite.Get();
                Image.color = Color.white;
            }
            else
            {
                Image.sprite = LockedSprite;
                Image.color = transform.parent.GetComponentInParent<Image>().color; // FIXME horrible hack, color does not update when UI theme changes.
            }
        }

        public GameObject InstantiateToolTip()
        {
            GameObject toolTip = Instantiate(ToolTipPrefab);
            Text title = toolTip.transform.Find("Main/Title").GetComponent<Text>();
            Text description = toolTip.transform.Find("Main/Description").GetComponent<Text>();
            Transform reward = toolTip.transform.Find("Reward");
            Text rewardText = toolTip.transform.Find("Reward/Text").GetComponent<Text>();
            Transform funFact = toolTip.transform.Find("FunFact");
            Text funFactText = toolTip.transform.Find("FunFact/Text").GetComponent<Text>();
            Text achievedOn = toolTip.transform.Find("AchievedOn").GetComponent<Text>();

            if (_isAchievementUnlocked)
            {
                achievedOn.text = "Achieved on: " + ProfileManager.CurrentProfile.GetAchievementStatus(_achievement.Identifier).CompletedOn.ToString(Localization.GetCurrentCulture());
            }
            else
            {
                achievedOn.gameObject.SetActive(false);
            }

            if (!_isAchievementUnlocked && _achievement.Hidden)
            {
                title.text = "Hidden achievement";
                description.text = "You have to unlock this to know what it's about.";

                reward.gameObject.SetActive(false);
                rewardText.gameObject.SetActive(false);
                funFact.gameObject.SetActive(false);
                funFactText.gameObject.SetActive(false);
            }
            else
            {
                title.text = _achievement.Name;
                description.text = _achievement.Description;

                if (_isAchievementUnlocked)
                {
                    if (string.IsNullOrEmpty(_achievement.RewardDescription))
                    {
                        reward.gameObject.SetActive(false);
                    }
                    else
                    {
                        rewardText.text = _achievement.RewardDescription;
                    }

                    if (string.IsNullOrEmpty(_achievement.FunFact))
                    {
                        funFact.gameObject.SetActive(false);
                    }
                    else
                    {
                        funFactText.text = _achievement.FunFact;
                    }
                }
                else
                {
                    reward.gameObject.SetActive(false);
                    funFact.gameObject.SetActive(false);
                }
            }
            return toolTip;
        }
    }
}
