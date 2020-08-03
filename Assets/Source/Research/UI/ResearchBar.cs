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
    public class ResearchBar : MonoBehaviour, ITooltip
    {
        public ResearchOption Research { get; private set; }
        public Text NameText;
        public Slider ProgressSlider;

        public bool IsCompleted { get; private set; }

        public string Title => Research.Name;
        public string Description => Research.Description;
        public string Footnote => "Progress: " + Mathf.RoundToInt(Research.Progress * 100) + "%";

        public void Assign (ResearchOption option)
        {
            Research = option;

            NameText.text = option.Name;
            Research.OnProgressed += OnProgressed;
            Research.OnCompleted += OnCompleted;

            ProgressSlider.value = option.Progress;
        }

        private void OnCompleted(ResearchOption obj)
        {
            UIGraphicStylizer stylizer = GetComponent<UIGraphicStylizer>();
            stylizer.Slot = BFA2.UI.Style.UIStyle.Slot.Highlight;
            stylizer.ApplyStyle();
            IsCompleted = true;
        }

        private void OnProgressed(ResearchOption obj)
        {
            ProgressSlider.value = Mathf.Clamp01 (obj.Progress);
        }

        public void Destroy ()
        {
            Research.OnProgressed -= OnProgressed;
            Research.OnCompleted -= OnCompleted;

            Destroy(gameObject);
        }
    }
}
