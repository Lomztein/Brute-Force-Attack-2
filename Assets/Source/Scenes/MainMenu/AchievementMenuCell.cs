using Lomztein.BFA2.Player.Progression.Achievements;
using Lomztein.BFA2.UI.Tooltip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Scenes.MainMenu
{
    public class AchievementMenuCell : MonoBehaviour, ITooltip
    {
        public Image Image;
        public Sprite LockedSprite;

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Footnote { get; private set; }

        public void Assign (Achievement achievement, bool unlocked)
        {
            if (unlocked)
            {
                Image.sprite = achievement.Sprite.Get();
                Footnote = "Achieved on: Not implemented yet lol";
            }
            else
            {
                Image.sprite = LockedSprite;
            }

            if (!unlocked && achievement.Hidden)
            {
                Title = "Hidden achievement";
                Description = "You have to unlock this to know what it's about.";
            }
            else
            {
                Title = achievement.Name;
                Description = achievement.Description;
            }
        }
    }
}
