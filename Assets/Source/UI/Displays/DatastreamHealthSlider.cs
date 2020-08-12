using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI.Displays
{
    public class DatastreamHealthSlider : HealthSlider
    {
        public Text Text;
        public string HealthText;

        protected override void OnHealthChanged(float prev, float cur, float max)
        {
            base.OnHealthChanged(prev, cur, max);
            Text.text = HealthText.Replace("{0}", Mathf.RoundToInt(_healthContainer.GetMaxHealth () - cur).ToString());
        }
    }
}
