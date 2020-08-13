using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class DatastreamHealthSlider : HealthSlider, ITooltip
    {
        public Text Text;
        public string HealthText;

        public string Title {get; private set;}

        public string Description => null;
        public string Footnote => null;

        protected override void OnHealthChanged(float prev, float cur, float max)
        {
            base.OnHealthChanged(prev, cur, max);
            Text.text = HealthText.Replace("{0}", Mathf.RoundToInt(_healthContainer.GetMaxHealth () - cur).ToString());
            SetTooltip(cur);
        }

        private void SetTooltip(float health)
        {
            if (health >= 99.9f)
            {
                Title = "Lookin' good!";
                return;
            }
            if (health >= 95f)
            {
                Title = "You've taken a hit but it's cool.";
                return;
            }
            if (health >= 80f)
            {
                Title = "Bit rough but still going strong!";
                return;
            }
            if (health >= 65f)
            {
                Title = "Perhaps you should start to worry a little.";
                return;
            }
            if (health >= 50f)
            {
                Title = "It's getting a bit too precarious for comfort.";
                return;
            }
            if (health >= 30f)
            {
                Title = "Do you ever feel like a plastic bag?.";
                return;
            }
            if (health >= 29)
            {
                Title = "Please improve your defenses NOW.";
                return;
            }
            if (health >= 10)
            {
                Title = "Prayin' to ROBO-JESUS!";
                return;
            }
            if (health >= 5f)
            {
                Title = "AAAAAAAAAAAAAAAAAAAAAAA";
                return;
            }
            if (health >= 1f)
            {
                Title = "Ah you did well mate, but it's the end for us.";
                return;
            }
            if (health > 0f)
            {
                Title = "YOU ARE DEAD, NOT BIG SURPRISE.";
            }
        }
    }
}
