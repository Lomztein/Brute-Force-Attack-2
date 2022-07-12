using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class DatastreamHealthSlider : HealthSlider, IHasToolTip
    {
        public Text Text;
        public string HealthText;

        public GameObject GetToolTip()
        {
            return SimpleToolTip.InstantiateToolTip(GetTooltip(_healthContainer.GetCurrentHealth()));
        }

        protected override void OnHealthChanged(float prev, float cur, float max)
        {
            base.OnHealthChanged(prev, cur, max);
            Text.text = HealthText.Replace("{0}", Mathf.RoundToInt(_healthContainer.GetMaxHealth () - cur).ToString());
            GetTooltip(cur);
        }

        private string GetTooltip(float health)
        {
            string title = null;
            if (health >= 99.9f)
            {
                title = "Lookin' good!";
            }
            if (health >= 95f)
            {
                title = "You've taken a hit but it's cool.";
            }
            if (health >= 80f)
            {
                title = "Bit rough but still going strong!";
            }
            if (health >= 65f)
            {
                title = "Perhaps you should start to worry a little.";
            }
            if (health >= 50f)
            {
                title = "It's getting a bit too precarious for comfort.";
            }
            if (health >= 30f)
            {
                title = "Do you ever feel like a plastic bag?.";
            }
            if (health >= 29)
            {
                title = "Please improve your defenses NOW.";
            }
            if (health >= 10)
            {
                title = "Prayin' to ROBO-JESUS!";
            }
            if (health >= 5f)
            {
                title = "AAAAAAAAAAAAAAAAAAAAAAA";
            }
            if (health >= 1f)
            {
                title = "Ah you did well mate, but it's the end for us.";
            }
            if (health > 0f)
            {
                title = "YOU ARE DEAD, NOT BIG SURPRISE.";
            }
            return title;
        }
    }
}
