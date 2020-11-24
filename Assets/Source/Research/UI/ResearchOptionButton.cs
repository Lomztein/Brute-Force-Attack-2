﻿using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.UI.Style.Stylizers;
using Lomztein.BFA2.UI.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.Research.UI
{
    public class ResearchOptionButton : MonoBehaviour, ITooltip
    {
        public string Title => _research.Name;
        public string Description => _research.Description;
        public string Footnote => string.Join("\n", _research.GetUniqueRequirementsStatuses());

        public Image Image;
        public Text Name;
        public Text Cost;
        public Button Button;

        private ResearchOption _research;

        public void Assign (ResearchOption option)
        {
            _research = option;

            Image.sprite = option.Sprite.Get();
            Image.color = option.SpriteTint;

            Name.text = option.Name;
            Cost.text = "Cost: " + option.ResourceCost.ToString();
            if (option.TimeCost != 0)
            {
                Cost.text += ", Waves: " + option.TimeCost;
            }
        }

        // Feels a bit strange to bring the container here, but it's easy to implement and change.
        public void UpdateAffordability (IResourceContainer container)
        {
            Button.interactable = container.HasEnough(_research.ResourceCost);
        }
    }
}
