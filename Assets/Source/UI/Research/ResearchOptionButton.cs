using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Style.Stylizers;
using Lomztein.BFA2.UI.ToolTip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Research.UI
{
    public class ResearchOptionButton : MonoBehaviour, IHasToolTip
    {
        public Image Image;
        public Text Name;
        public Text Cost;
        public Button Button;

        public ResearchOption Research { get; private set; }

        public void Assign (ResearchOption option)
        {
            Research = option;
            UpdateButton(true);
        }

        public GameObject GetToolTip()
        {
            return SimpleToolTip.InstantiateToolTip(Research.Name, Research.Description, Research.TimeCost == 0 ? "Time: Instant" : "Time: " + Research.TimeCost + " Waves");
        }

        // Feels a bit strange to bring the container here, but it's easy to implement and change.
        public void UpdateButton (bool available)
        {
            Button.interactable = available;
            Color c = Research.SpriteTint;
            float alpha = available ? 1 : 0.5f;
            Image.color = new Color(c.r, c.g, c.b, c.a * alpha);

            Image.sprite = Research.Sprite.Get();
            Image.color = Research.SpriteTint;

            Name.text = Research.Name;

            if (Research.UniquePrerequisitesCompleted)
            {
                Cost.text = Research.ResourceCost.Format(false);
            }
        }
    }
}
